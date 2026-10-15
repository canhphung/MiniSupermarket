using MiniSupermarket.API.DTOs;

namespace MiniSupermarket.API.Services;

public sealed class CheckoutException : Exception
{
    public int StatusCode { get; }
    public CheckoutException(string message, int statusCode = 400) : base(message)
        => StatusCode = statusCode;
}

/// <summary>Quy tắc thuần C#, có thể kiểm tra mà không kết nối SQL Server.</summary>
public static class CheckoutRules
{
    public const decimal MaxMoney = 9999999999999999.99m;

    public static List<CheckoutLine> Normalize(CheckoutRequest request)
    {
        if (request.Items == null || request.Items.Count is < 1 or > 100)
            throw new CheckoutException("Giỏ hàng phải có từ 1 đến 100 dòng.");
        if (request.CashReceived < 0 || request.CashReceived > MaxMoney
            || decimal.Round(request.CashReceived, 2) != request.CashReceived)
            throw new CheckoutException("Tiền khách đưa không hợp lệ.");
        if ((request.CustomerPhone?.Trim().Length ?? 0) > 15)
            throw new CheckoutException("Số điện thoại tối đa 15 ký tự.");
        if (request.Items.Any(i => i == null || i.ProductId <= 0
            || i.Quantity is < 1 or > 100000 || i.ExpectedUnitPrice < 0
            || i.ExpectedUnitPrice > MaxMoney))
            throw new CheckoutException("Mã sản phẩm, số lượng hoặc đơn giá không hợp lệ.");

        return request.Items.GroupBy(i => i.ProductId).OrderBy(g => g.Key).Select(g =>
        {
            if (g.Select(i => i.ExpectedUnitPrice).Distinct().Count() != 1)
                throw new CheckoutException("Các dòng cùng sản phẩm phải có cùng đơn giá.");
            return new CheckoutLine(g.Key, checked(g.Sum(i => i.Quantity)), g.First().ExpectedUnitPrice);
        }).ToList();
    }

    public static int GetRewardPoints(decimal total, bool hasCustomer)
    {
        if (total < 0 || total > MaxMoney)
            throw new CheckoutException("Tổng tiền vượt giới hạn hóa đơn.");
        if (!hasCustomer) return 0;
        decimal points = decimal.Floor(total / 10000m);
        if (points > int.MaxValue)
            throw new CheckoutException("Điểm thưởng vượt giới hạn lưu trữ.");
        return (int)points;
    }
}
