using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Services;
using System.Security.Claims;

namespace MiniSupermarket.API.Controllers;

[ApiController]
[Route("api/orders")]
[Authorize(Roles = "Admin,Cashier")]
public class OrdersController : ControllerBase
{
    private readonly AdoCheckoutService _checkout;
    private readonly AdoReportService _reports;
    private readonly ILogger<OrdersController> _logger;
    public OrdersController(AdoCheckoutService checkout, AdoReportService reports, ILogger<OrdersController> logger)
        => (_checkout, _reports, _logger) = (checkout, reports, logger);

    [HttpPost("checkout")]
    [HttpPost("/api/checkout")]
    public async Task<IActionResult> Checkout(CheckoutRequest request, CancellationToken ct)
    {
        if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int cashierId)) return Unauthorized();
        return await ExecuteAsync(async () => await _checkout.CheckoutAsync(cashierId, request, ct));
    }

    [HttpDelete("{orderId:int}")]
    [HttpDelete("/api/checkout/cancel/{orderId:int}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Cancel(int orderId, CancellationToken ct)
        => await ExecuteAsync(async () => await _checkout.CancelAsync(orderId, ct));

    [HttpGet, Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetOrders([FromQuery] DateOnly fromDate, [FromQuery] DateOnly toDate, CancellationToken ct)
        => await ExecuteAsync(async () => await _reports.OrdersAsync(fromDate, toDate, ct));

    private async Task<IActionResult> ExecuteAsync(Func<Task<object>> action)
    {
        try { return Ok(await action()); }
        catch (CheckoutException ex) { return StatusCode(ex.StatusCode, new { message = ex.Message }); }
        catch (SqlException ex) when (ex.Number == 1205)
        {
            return Conflict(new { message = "Giao dịch xung đột và đã được SQL Server hoàn tác. Hãy thử lại." });
        }
        catch (SqlException ex)
        {
            _logger.LogError(ex, "Lỗi SQL trong nghiệp vụ hóa đơn.");
            return StatusCode(500, new { message = "Không thể xử lý hóa đơn. Kiểm tra kết nối và migration database." });
        }
    }
}
