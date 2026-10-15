using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MiniSupermarket.API.Data;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/reports")]
    [Authorize(Roles = "Admin")]
    public class ReportsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ReportsController(SupermarketDbContext context)
        {
            _context = context;
        }

        // GET /api/reports/daily?date=2026-10-15
        [HttpGet("daily")]
        public async Task<ActionResult<DailyRevenueResponse>> GetDaily(
            [FromQuery] DateOnly? date,
            CancellationToken cancellationToken)
        {
            if (!date.HasValue
                || date.Value == DateOnly.MinValue
                || date.Value == DateOnly.MaxValue)
            {
                return BadRequest(new
                {
                    message = "Ngày báo cáo không hợp lệ. Dùng yyyy-MM-dd."
                });
            }

            DateOnly reportDate = date.Value;

            // Khoảng thời gian của ngày được chọn tại Việt Nam (UTC+7).
            DateTime startLocal =
                reportDate.ToDateTime(TimeOnly.MinValue);

            DateTime startUtc = new DateTimeOffset(
                startLocal,
                TimeSpan.FromHours(7)).UtcDateTime;

            DateTime endUtc = startUtc.AddDays(1);

            // Dùng khoảng [đầu ngày, đầu ngày kế tiếp).
            var orders = _context.Orders
                .AsNoTracking()
                .Where(order =>
                    order.CreatedAtUtc >= startUtc
                    && order.CreatedAtUtc < endUtc);

            int totalOrders = await orders.CountAsync(
                cancellationToken);

            decimal totalRevenue = await orders
                .SumAsync(
                    order => (decimal?)order.TotalAmount,
                    cancellationToken)
                ?? 0m;

            var items = _context.OrderItems
                .AsNoTracking()
                .Where(item =>
                    item.Order.CreatedAtUtc >= startUtc
                    && item.Order.CreatedAtUtc < endUtc);

            // Nhóm theo mã để việc đổi tên sản phẩm
            // không tách số lượng bán thành nhiều nhóm.
            var topProduct = await items
                .GroupBy(item => item.ProductId)
                .Select(group => new
                {
                    ProductId = group.Key,
                    Quantity = group.Sum(item => (long)item.Quantity)
                })
                .OrderByDescending(product => product.Quantity)
                .ThenBy(product => product.ProductId)
                .FirstOrDefaultAsync(cancellationToken);

            string bestSeller = "Chưa có dữ liệu";
            long bestSellerQuantity = 0;

            if (topProduct != null)
            {
                // Lấy tên đã lưu trong dòng hóa đơn gần nhất.
                bestSeller = await items
                    .Where(item =>
                        item.ProductId == topProduct.ProductId)
                    .OrderByDescending(item => item.OrderItemId)
                    .Select(item => item.ProductName)
                    .FirstAsync(cancellationToken);

                bestSellerQuantity = topProduct.Quantity;
            }

            return Ok(new DailyRevenueResponse
            {
                Date = reportDate,
                TotalOrders = totalOrders,
                TotalRevenue = totalRevenue,
                BestSeller = bestSeller,
                BestSellerQuantity = bestSellerQuantity
            });
        }
    }

    public class DailyRevenueResponse
    {
        public DateOnly Date { get; set; }

        public int TotalOrders { get; set; }

        public decimal TotalRevenue { get; set; }

        public string BestSeller { get; set; } = string.Empty;

        public long BestSellerQuantity { get; set; }
    }
}