using Microsoft.Data.SqlClient;
using MiniSupermarket.API.DTOs;
using System.Data;

namespace MiniSupermarket.API.Services;

/// <summary>Tất cả lệnh ghi của một nghiệp vụ dùng chung connection và SqlTransaction.</summary>
public sealed class AdoCheckoutService
{
    private readonly SqlConnectionFactory _factory;
    private readonly ILogger<AdoCheckoutService> _logger;
    public AdoCheckoutService(SqlConnectionFactory factory, ILogger<AdoCheckoutService> logger)
        => (_factory, _logger) = (factory, logger);

    public async Task<CheckoutResponse> CheckoutAsync(int cashierId, CheckoutRequest request, CancellationToken ct)
    {
        var lines = CheckoutRules.Normalize(request);
        await using var connection = _factory.Create();
        await connection.OpenAsync(ct);
        await using var transaction = (SqlTransaction)await connection.BeginTransactionAsync(ct);
        try
        {
            using (var command = new SqlCommand("SELECT Role, IsActive FROM dbo.Users WITH (HOLDLOCK) WHERE Id = @Id", connection, transaction))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = cashierId;
                await using var reader = await command.ExecuteReaderAsync(ct);
                if (!await reader.ReadAsync(ct) || !reader.GetBoolean(1)
                    || !(reader.GetString(0).Equals("Admin", StringComparison.OrdinalIgnoreCase)
                        || reader.GetString(0).Equals("Cashier", StringComparison.OrdinalIgnoreCase)))
                    throw new CheckoutException("Tài khoản thu ngân không hợp lệ.", 401);
            }

            int? customerId = null;
            string phone = request.CustomerPhone?.Trim() ?? string.Empty;
            if (phone.Length > 0)
            {
                using var command = new SqlCommand("SELECT TOP (2) CustomerId FROM dbo.Customers WITH (UPDLOCK, HOLDLOCK) WHERE PhoneNumber = @Phone ORDER BY CustomerId", connection, transaction);
                command.Parameters.Add("@Phone", SqlDbType.VarChar, 15).Value = phone;
                await using var reader = await command.ExecuteReaderAsync(ct);
                if (!await reader.ReadAsync(ct)) throw new CheckoutException("Số điện thoại chưa được đăng ký.");
                customerId = reader.GetInt32(0);
                if (await reader.ReadAsync(ct)) throw new CheckoutException("Số điện thoại trùng nhiều khách hàng. Hãy sửa dữ liệu trước khi bán.", 409);
            }

            // Khóa sản phẩm theo thứ tự mã tăng dần để hạn chế deadlock.
            var products = new Dictionary<int, (string Name, decimal Price)>();
            decimal total = 0;
            foreach (var line in lines)
            {
                using var command = new SqlCommand("SELECT ProductName, Price FROM dbo.Products WITH (UPDLOCK, HOLDLOCK) WHERE ProductId = @Id", connection, transaction);
                command.Parameters.Add("@Id", SqlDbType.Int).Value = line.ProductId;
                await using var reader = await command.ExecuteReaderAsync(ct);
                if (!await reader.ReadAsync(ct)) throw new CheckoutException($"Không tìm thấy sản phẩm #{line.ProductId}.");
                var product = (Name: reader.GetString(0), Price: reader.GetDecimal(1));
                if (product.Price < 0 || product.Price != line.ExpectedUnitPrice)
                    throw new CheckoutException($"Giá {product.Name} đã thay đổi. Hãy quét lại sản phẩm.", 409);
                products.Add(line.ProductId, product);
                total += product.Price * line.Quantity;
            }
            int points = CheckoutRules.GetRewardPoints(total, customerId.HasValue);
            if (request.CashReceived < total) throw new CheckoutException("Tiền khách đưa chưa đủ.");

            int orderId;
            using (var command = new SqlCommand("""
                INSERT INTO dbo.Orders (CreatedAtUtc, CashierId, CustomerId, TotalAmount, CashReceived, ChangeAmount, RewardPointsAwarded)
                OUTPUT INSERTED.OrderId
                VALUES (@Created, @Cashier, @Customer, @Total, @Cash, @Change, @Points)
                """, connection, transaction))
            {
                command.Parameters.Add("@Created", SqlDbType.DateTime2).Value = DateTime.UtcNow;
                command.Parameters.Add("@Cashier", SqlDbType.Int).Value = cashierId;
                command.Parameters.Add("@Customer", SqlDbType.Int).Value = (object?)customerId ?? DBNull.Value;
                command.Parameters.Add("@Points", SqlDbType.Int).Value = points;
                SqlConnectionFactory.Money(command, "@Total", total);
                SqlConnectionFactory.Money(command, "@Cash", request.CashReceived);
                SqlConnectionFactory.Money(command, "@Change", request.CashReceived - total);
                orderId = Convert.ToInt32(await command.ExecuteScalarAsync(ct));
            }

            foreach (var line in lines)
            {
                var product = products[line.ProductId];
                // Điều kiện ở UPDATE bảo đảm tồn kho không xuống âm ngay cả khi bán đồng thời.
                using (var stock = new SqlCommand("""
                    UPDATE dbo.Products SET StockQuantity = StockQuantity - @Quantity
                    WHERE ProductId = @Id AND StockQuantity >= @Quantity
                    """, connection, transaction))
                {
                    stock.Parameters.Add("@Id", SqlDbType.Int).Value = line.ProductId;
                    stock.Parameters.Add("@Quantity", SqlDbType.Int).Value = line.Quantity;
                    if (await stock.ExecuteNonQueryAsync(ct) != 1)
                        throw new CheckoutException($"{product.Name} không đủ tồn kho. Giao dịch đã hủy.");
                }
                using var detail = new SqlCommand("""
                    INSERT INTO dbo.OrderItems (OrderId, ProductId, ProductName, Quantity, UnitPrice, TotalPrice)
                    VALUES (@Order, @Product, @Name, @Quantity, @Price, @Total)
                    """, connection, transaction);
                detail.Parameters.Add("@Order", SqlDbType.Int).Value = orderId;
                detail.Parameters.Add("@Product", SqlDbType.Int).Value = line.ProductId;
                detail.Parameters.Add("@Name", SqlDbType.NVarChar, 150).Value = product.Name;
                detail.Parameters.Add("@Quantity", SqlDbType.Int).Value = line.Quantity;
                SqlConnectionFactory.Money(detail, "@Price", product.Price);
                SqlConnectionFactory.Money(detail, "@Total", product.Price * line.Quantity);
                await detail.ExecuteNonQueryAsync(ct);
            }

            if (customerId.HasValue && points > 0)
            {
                using var command = new SqlCommand("""
                    UPDATE dbo.Customers SET RewardPoints = RewardPoints + @Points
                    WHERE CustomerId = @Id AND RewardPoints <= 2147483647 - @Points
                    """, connection, transaction);
                command.Parameters.Add("@Id", SqlDbType.Int).Value = customerId.Value;
                command.Parameters.Add("@Points", SqlDbType.Int).Value = points;
                if (await command.ExecuteNonQueryAsync(ct) != 1)
                    throw new CheckoutException("Không thể cộng điểm thưởng cho khách hàng.", 409);
            }
            await transaction.CommitAsync(ct);
            return new CheckoutResponse(orderId, total, request.CashReceived,
                request.CashReceived - total, points, lines.Sum(l => l.Quantity));
        }
        catch
        {
            await RollbackAsync(transaction);
            throw;
        }
    }

    public async Task<CancelOrderResponse> CancelAsync(int orderId, CancellationToken ct)
    {
        await using var connection = _factory.Create();
        await connection.OpenAsync(ct);
        await using var transaction = (SqlTransaction)await connection.BeginTransactionAsync(ct);
        try
        {
            int? customerId;
            int points;
            using (var command = new SqlCommand("SELECT CustomerId, RewardPointsAwarded FROM dbo.Orders WITH (UPDLOCK, HOLDLOCK) WHERE OrderId = @Id", connection, transaction))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = orderId;
                await using var reader = await command.ExecuteReaderAsync(ct);
                if (!await reader.ReadAsync(ct)) throw new CheckoutException("Không tìm thấy hóa đơn hoặc hóa đơn đã được hủy.", 404);
                customerId = reader.IsDBNull(0) ? null : reader.GetInt32(0);
                points = reader.GetInt32(1);
            }

            int deducted = 0;
            if (customerId.HasValue && points > 0)
            {
                // Khóa khách trước sản phẩm, cùng thứ tự với checkout.
                using var command = new SqlCommand("""
                    UPDATE dbo.Customers SET RewardPoints = CASE WHEN RewardPoints >= @Points
                        THEN RewardPoints - @Points ELSE 0 END
                    OUTPUT DELETED.RewardPoints - INSERTED.RewardPoints
                    WHERE CustomerId = @Id
                    """, connection, transaction);
                command.Parameters.Add("@Id", SqlDbType.Int).Value = customerId.Value;
                command.Parameters.Add("@Points", SqlDbType.Int).Value = points;
                var value = await command.ExecuteScalarAsync(ct);
                if (value == null) throw new CheckoutException("Khách hàng của hóa đơn không còn tồn tại.", 409);
                deducted = Convert.ToInt32(value);
            }

            var items = new List<(int Id, int Quantity)>();
            using (var command = new SqlCommand("SELECT ProductId, SUM(Quantity) FROM dbo.OrderItems WHERE OrderId = @Id GROUP BY ProductId ORDER BY ProductId", connection, transaction))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = orderId;
                await using var reader = await command.ExecuteReaderAsync(ct);
                while (await reader.ReadAsync(ct)) items.Add((reader.GetInt32(0), reader.GetInt32(1)));
            }
            foreach (var item in items)
            {
                using var command = new SqlCommand("""
                    UPDATE dbo.Products SET StockQuantity = StockQuantity + @Quantity
                    WHERE ProductId = @Id AND StockQuantity <= 2147483647 - @Quantity
                    """, connection, transaction);
                command.Parameters.Add("@Id", SqlDbType.Int).Value = item.Id;
                command.Parameters.Add("@Quantity", SqlDbType.Int).Value = item.Quantity;
                if (await command.ExecuteNonQueryAsync(ct) != 1)
                    throw new CheckoutException("Không thể hoàn kho. Toàn bộ thao tác hủy sẽ được hoàn tác.", 409);
            }
            using (var command = new SqlCommand("DELETE FROM dbo.OrderItems WHERE OrderId = @Id; DELETE FROM dbo.Orders WHERE OrderId = @Id;", connection, transaction))
            {
                command.Parameters.Add("@Id", SqlDbType.Int).Value = orderId;
                await command.ExecuteNonQueryAsync(ct);
            }
            await transaction.CommitAsync(ct);
            return new CancelOrderResponse(orderId, deducted);
        }
        catch
        {
            await RollbackAsync(transaction);
            throw;
        }
    }

    private async Task RollbackAsync(SqlTransaction transaction)
    {
        try { await transaction.RollbackAsync(CancellationToken.None); }
        catch (Exception ex) { _logger.LogError(ex, "Không thể xác nhận rollback transaction."); }
    }
}
