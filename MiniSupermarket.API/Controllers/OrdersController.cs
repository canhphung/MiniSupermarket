using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/orders")]
    [Authorize(Roles = "Admin,Cashier")]
    public class OrdersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public OrdersController(SupermarketDbContext context)
        {
            _context = context;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout(
            [FromBody] CheckoutRequest request)
        {
            if (!int.TryParse(
                User.FindFirstValue(ClaimTypes.NameIdentifier),
                out int cashierId))
            {
                return Unauthorized();
            }

            // Lưu hóa đơn và trừ kho trong cùng một transaction.
            await using var transaction =
                await _context.Database.BeginTransactionAsync(
                    IsolationLevel.Serializable);

            var cashier = await _context.Users.FindAsync(cashierId);

            if (cashier == null || !cashier.IsActive)
            {
                return Unauthorized();
            }

            int? customerId = null;
            string phone = request.CustomerPhone.Trim();

            if (!string.IsNullOrWhiteSpace(phone))
            {
                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.PhoneNumber == phone);

                if (customer == null)
                {
                    return BadRequest(new
                    {
                        message = "Số điện thoại khách hàng chưa được đăng ký!"
                    });
                }

                customerId = customer.CustomerId;
            }

            var productIds = request.Items
                .Select(item => item.ProductId)
                .Distinct()
                .ToList();

            var products = await _context.Products
                .Where(product => productIds.Contains(product.ProductId))
                .ToDictionaryAsync(product => product.ProductId);

            var order = new Order
            {
                CreatedAtUtc = DateTime.UtcNow,
                CashierId = cashier.Id,
            //    CashierUsername = cashier.Username,
                CustomerId = customerId,
                CashReceived = request.CashReceived
            };

            foreach (var group in request.Items.GroupBy(i => i.ProductId))
            {
                if (!products.TryGetValue(group.Key, out var product))
                {
                    return BadRequest(new
                    {
                        message = $"Sản phẩm ID {group.Key} không tồn tại!"
                    });
                }

                int quantity = group.Sum(item => item.Quantity);

                if (product.StockQuantity < quantity)
                {
                    return Conflict(new
                    {
                        message =
                            $"\"{product.ProductName}\" chỉ còn " +
                            $"{product.StockQuantity} sản phẩm!"
                    });
                }

                if (product.Price < 0
                    || group.Any(item =>
                        item.ExpectedUnitPrice != product.Price))
                {
                    return Conflict(new
                    {
                        message =
                            $"Giá \"{product.ProductName}\" đã thay đổi " +
                            "hoặc không hợp lệ. Hãy hủy giỏ và quét lại!"
                    });
                }

                order.Items.Add(new OrderItem
                {
                    ProductId = product.ProductId,
                    ProductName = product.ProductName,
                    Quantity = quantity,
                    UnitPrice = product.Price,
                    TotalPrice = product.Price * quantity
                });

                product.StockQuantity -= quantity;
            }

            order.TotalAmount = order.Items.Sum(item => item.TotalPrice);

            if (request.CashReceived < order.TotalAmount)
            {
                return BadRequest(new
                {
                    message = "Tiền khách đưa chưa đủ để thanh toán!"
                });
            }

            order.ChangeAmount =
                request.CashReceived - order.TotalAmount;

            _context.Orders.Add(order);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return Ok(new
            {
                order.OrderId,
                order.TotalAmount,
                order.CashReceived,
                order.ChangeAmount
            });
        }
    }

    public class CheckoutRequest
    {
        [StringLength(20)]
        public string CustomerPhone { get; set; } = string.Empty;

        [Range(typeof(decimal), "0", "9999999999999999")]
        public decimal CashReceived { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(100)]
        public List<CheckoutItemRequest> Items { get; set; } = new();
    }

    public class CheckoutItemRequest
    {
        [Range(1, int.MaxValue)]
        public int ProductId { get; set; }

        [Range(1, 100000)]
        public int Quantity { get; set; }

        public decimal ExpectedUnitPrice { get; set; }
    }
}