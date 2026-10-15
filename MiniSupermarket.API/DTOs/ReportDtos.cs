namespace MiniSupermarket.API.DTOs;

public record CashierRevenueDto(string CashierUsername, long TotalOrders,
    decimal TotalRevenue, long TotalPointsAwarded);
public record TopProductDto(int ProductId, string ProductName, long TotalQuantity, decimal TotalRevenue);
public record OrderSummaryDto(int OrderId, DateTime CreatedAtUtc, string CashierUsername,
    string? CustomerPhone, decimal TotalAmount, int RewardPointsAwarded);
public class ProductLookupDto
{
    public int ProductId { get; set; }
    public string Barcode { get; set; } = string.Empty;
    public string ProductName { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
}
