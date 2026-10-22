using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.DTOs;

public class CheckoutRequest
{
    [StringLength(15)]
    public string CustomerPhone { get; set; } = string.Empty;

    [Range(typeof(decimal), 
        "0",
        "9999999999999999.99",
        ParseLimitsInInvariantCulture = true,
        ConvertValueInInvariantCulture = true)]
    public decimal CashReceived { get; set; }

    [Required, MinLength(1), MaxLength(100)]
    public List<CheckoutItemRequest> Items { get; set; } = new();
}

public class CheckoutItemRequest
{
    [Range(1, int.MaxValue)]
    public int ProductId { get; set; }
    [Range(1, 100000)]
    public int Quantity { get; set; }
    [Range(typeof(decimal), "0", "9999999999999999.99",
        ParseLimitsInInvariantCulture = true,
        ConvertValueInInvariantCulture = true
        )]
    public decimal ExpectedUnitPrice { get; set; }
}

public record CheckoutLine(int ProductId, int Quantity, decimal ExpectedUnitPrice);
public record CheckoutResponse(int OrderId, decimal TotalAmount, decimal CashReceived,
    decimal ChangeAmount, int RewardPointsAdded, int TotalItems);
public record CancelOrderResponse(int OrderId, int RewardPointsDeducted);
