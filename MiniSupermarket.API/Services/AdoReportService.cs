using Microsoft.Data.SqlClient;
using MiniSupermarket.API.DTOs;
using System.Data;

namespace MiniSupermarket.API.Services;

public sealed class AdoReportService
{
    private readonly SqlConnectionFactory _factory;
    public AdoReportService(SqlConnectionFactory factory) => _factory = factory;

    public static (DateTime StartUtc, DateTime EndUtc) DateRange(DateOnly from, DateOnly to)
        => VietnamDateRange.Get(from, to);

    public async Task<List<CashierRevenueDto>> RevenueAsync(DateOnly? from, DateOnly? to, CancellationToken ct)
    {
        if (from.HasValue != to.HasValue) throw new CheckoutException("Cần nhập cả ngày bắt đầu và kết thúc.");
        await using var connection = _factory.Create();
        using var command = new SqlCommand(from.HasValue ? "dbo.sp_GetRevenueByDateRange" : "dbo.sp_GetRevenueByCashier", connection)
        { CommandType = CommandType.StoredProcedure };
        if (from.HasValue)
        {
            var (start, end) = DateRange(from.Value, to!.Value);
            command.Parameters.Add("@FromDate", SqlDbType.DateTime2).Value = start;
            command.Parameters.Add("@ToDate", SqlDbType.DateTime2).Value = end;
        }
        await connection.OpenAsync(ct);
        await using var reader = await command.ExecuteReaderAsync(ct);
        var result = new List<CashierRevenueDto>();
        while (await reader.ReadAsync(ct)) result.Add(new CashierRevenueDto(
            reader.GetString(0), reader.IsDBNull(1) ? 0 : reader.GetInt64(1),
            reader.IsDBNull(2) ? 0m : reader.GetDecimal(2), reader.IsDBNull(3) ? 0 : reader.GetInt64(3)));
        return result;
    }

    public async Task<List<TopProductDto>> TopProductsAsync(DateOnly? from, DateOnly? to, CancellationToken ct)
    {
        if (from.HasValue != to.HasValue) throw new CheckoutException("Cần nhập cả ngày bắt đầu và kết thúc.");
        await using var connection = _factory.Create();
        using var command = new SqlCommand("dbo.sp_GetTop5SellingProducts", connection) { CommandType = CommandType.StoredProcedure };
        DateTime? start = null, end = null;
        if (from.HasValue) (start, end) = DateRange(from.Value, to!.Value);
        command.Parameters.Add("@FromDate", SqlDbType.DateTime2).Value = (object?)start ?? DBNull.Value;
        command.Parameters.Add("@ToDate", SqlDbType.DateTime2).Value = (object?)end ?? DBNull.Value;
        await connection.OpenAsync(ct);
        await using var reader = await command.ExecuteReaderAsync(ct);
        var result = new List<TopProductDto>();
        while (await reader.ReadAsync(ct)) result.Add(new TopProductDto(reader.GetInt32(0), reader.GetString(1), reader.GetInt64(2), reader.GetDecimal(3)));
        return result;
    }

    public async Task<List<OrderSummaryDto>> OrdersAsync(DateOnly from, DateOnly to, CancellationToken ct)
    {
        var (start, end) = DateRange(from, to);
        await using var connection = _factory.Create();
        using var command = new SqlCommand("""
            SELECT TOP (200) o.OrderId, o.CreatedAtUtc, u.Username, c.PhoneNumber, o.TotalAmount, o.RewardPointsAwarded
            FROM dbo.Orders o JOIN dbo.Users u ON u.Id = o.CashierId
            LEFT JOIN dbo.Customers c ON c.CustomerId = o.CustomerId
            WHERE o.CreatedAtUtc >= @From AND o.CreatedAtUtc < @To
            ORDER BY o.CreatedAtUtc DESC, o.OrderId DESC
            """, connection);
        command.Parameters.Add("@From", SqlDbType.DateTime2).Value = start;
        command.Parameters.Add("@To", SqlDbType.DateTime2).Value = end;
        await connection.OpenAsync(ct);
        await using var reader = await command.ExecuteReaderAsync(ct);
        var result = new List<OrderSummaryDto>();
        while (await reader.ReadAsync(ct)) result.Add(new OrderSummaryDto(reader.GetInt32(0),
            DateTime.SpecifyKind(reader.GetDateTime(1), DateTimeKind.Utc), reader.GetString(2),
            reader.IsDBNull(3) ? null : reader.GetString(3), reader.GetDecimal(4), reader.GetInt32(5)));
        return result;
    }
}
