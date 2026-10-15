namespace MiniSupermarket.API.Services;

public static class VietnamDateRange
{
    public static (DateTime StartUtc, DateTime EndUtc) Get(DateOnly from, DateOnly to)
    {
        if (from > to || from == DateOnly.MinValue || to == DateOnly.MaxValue)
            throw new CheckoutException("Khoảng ngày không hợp lệ.");
        var start = new DateTimeOffset(from.ToDateTime(TimeOnly.MinValue), TimeSpan.FromHours(7)).UtcDateTime;
        var end = new DateTimeOffset(to.AddDays(1).ToDateTime(TimeOnly.MinValue), TimeSpan.FromHours(7)).UtcDateTime;
        return (start, end);
    }
}
