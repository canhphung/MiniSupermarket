using Microsoft.Data.SqlClient;
using MiniSupermarket.API.DTOs;
using System.Data;

namespace MiniSupermarket.API.Services;

public sealed class AdoProductService
{
    private readonly SqlConnectionFactory _factory;
    public AdoProductService(SqlConnectionFactory factory) => _factory = factory;
    private const string Columns = "ProductId, Barcode, ProductName, Price, StockQuantity";

    public async Task<List<ProductLookupDto>> ReadAsync(string? barcode, int? maxStock, CancellationToken ct)
    {
        await using var connection = _factory.Create();
        // Chỉ ghép các mệnh đề cố định; dữ liệu đầu vào luôn truyền qua parameter.
        string where = barcode != null ? " WHERE Barcode = @Barcode" : maxStock.HasValue ? " WHERE StockQuantity <= @MaxStock" : "";
        using var command = new SqlCommand($"SELECT {Columns} FROM dbo.Products{where} ORDER BY ProductId", connection);
        command.Parameters.Add("@Barcode", SqlDbType.NVarChar, 50).Value = (object?)barcode ?? DBNull.Value;
        command.Parameters.Add("@MaxStock", SqlDbType.Int).Value = (object?)maxStock ?? DBNull.Value;
        await connection.OpenAsync(ct);
        await using var reader = await command.ExecuteReaderAsync(ct);
        var products = new List<ProductLookupDto>();
        while (await reader.ReadAsync(ct)) products.Add(new ProductLookupDto
        {
            ProductId = reader.GetInt32(0), Barcode = reader.GetString(1),
            ProductName = reader.GetString(2), Price = reader.GetDecimal(3), StockQuantity = reader.GetInt32(4)
        });
        return products;
    }

    public async Task<long> CountAsync(CancellationToken ct)
    {
        await using var connection = _factory.Create();
        using var command = new SqlCommand("SELECT COUNT_BIG(*) FROM dbo.Products", connection);
        await connection.OpenAsync(ct);
        return Convert.ToInt64(await command.ExecuteScalarAsync(ct));
    }

    // SqlDataAdapter.Fill là ví dụ ngắt kết nối theo yêu cầu bài học.
    // Đây là snapshot mỗi request, không phải cache offline bền vững.
    public List<ProductLookupDto> DisconnectedSnapshot()
    {
        var table = new DataTable();
        using (var connection = _factory.Create())
        using (var command = new SqlCommand($"SELECT {Columns} FROM dbo.Products ORDER BY ProductId", connection))
        using (var adapter = new SqlDataAdapter(command)) adapter.Fill(table);
        return table.AsEnumerable().Select(row => new ProductLookupDto
        {
            ProductId = row.Field<int>("ProductId"), Barcode = row.Field<string>("Barcode") ?? "",
            ProductName = row.Field<string>("ProductName") ?? "", Price = row.Field<decimal>("Price"),
            StockQuantity = row.Field<int>("StockQuantity")
        }).ToList();
    }

    public object CategoryCounts()
    {
        var table = new DataTable();
        using (var connection = _factory.Create())
        using (var command = new SqlCommand("""
            SELECT c.CategoryId, c.CategoryName, COUNT(p.ProductId) AS Total
            FROM dbo.Categories c LEFT JOIN dbo.Products p ON p.CategoryId = c.CategoryId
            GROUP BY c.CategoryId, c.CategoryName ORDER BY c.CategoryId
            """, connection))
        using (var adapter = new SqlDataAdapter(command)) adapter.Fill(table);
        return table.AsEnumerable().Select(row => new
        {
            CategoryId = row.Field<int>("CategoryId"), CategoryName = row.Field<string>("CategoryName"),
            Total = row.Field<int>("Total")
        }).ToList();
    }

    public async Task<bool> SetStockAsync(int productId, int quantity, CancellationToken ct)
    {
        await using var connection = _factory.Create();
        using var command = new SqlCommand("UPDATE dbo.Products SET StockQuantity = @Stock WHERE ProductId = @Id", connection);
        command.Parameters.Add("@Stock", SqlDbType.Int).Value = quantity;
        command.Parameters.Add("@Id", SqlDbType.Int).Value = productId;
        await connection.OpenAsync(ct);
        return await command.ExecuteNonQueryAsync(ct) == 1;
    }
}
