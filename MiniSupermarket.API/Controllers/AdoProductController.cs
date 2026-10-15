using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MiniSupermarket.API.Services;
using System.ComponentModel.DataAnnotations;

namespace MiniSupermarket.API.Controllers;

[ApiController]
[Route("api/ado-products")]
[Authorize(Roles = "Admin,Cashier,Warehouse")]
public class AdoProductController : ControllerBase
{
    private readonly AdoProductService _service;
    public AdoProductController(AdoProductService service) => _service = service;

    [HttpGet("barcode/{barcode}")]
    public async Task<IActionResult> GetByBarcode(string barcode, CancellationToken ct)
    {
        if (string.IsNullOrWhiteSpace(barcode) || barcode.Trim().Length > 50)
            return BadRequest(new { message = "Mã vạch phải có từ 1 đến 50 ký tự." });
        var products = await _service.ReadAsync(barcode.Trim(), null, ct);
        if (products.Count == 0) return NotFound(new { message = "Không tìm thấy mã vạch." });
        if (products.Count > 1) return Conflict(new { message = "Mã vạch bị trùng. Hãy sửa dữ liệu sản phẩm." });
        return Ok(products[0]);
    }

    [HttpGet, Authorize(Roles = "Admin,Warehouse")]
    public async Task<IActionResult> GetAll(CancellationToken ct) => Ok(await _service.ReadAsync(null, null, ct));

    [HttpGet("count"), Authorize(Roles = "Admin,Warehouse")]
    public async Task<IActionResult> Count(CancellationToken ct) => Ok(new { total = await _service.CountAsync(ct) });

    [HttpGet("filter-stock"), Authorize(Roles = "Admin,Warehouse")]
    public async Task<IActionResult> FilterStock([FromQuery, Range(0, int.MaxValue)] int minStock = 50,
        CancellationToken ct = default) => Ok(await _service.ReadAsync(null, minStock, ct));

    [HttpGet("disconnected-cache"), Authorize(Roles = "Admin,Warehouse")]
    public IActionResult Snapshot()
    {
        var products = _service.DisconnectedSnapshot();
        return Ok(new { totalLoaded = products.Count, cachedAt = DateTime.UtcNow, data = products });
    }

    [HttpGet("category-counts"), Authorize(Roles = "Admin,Warehouse")]
    public IActionResult CategoryCounts() => Ok(_service.CategoryCounts());

    [HttpPatch("{productId:int}/stock"), Authorize(Roles = "Admin,Warehouse")]
    public async Task<IActionResult> SetStock(int productId, StockRequest request, CancellationToken ct)
        => await _service.SetStockAsync(productId, request.StockQuantity, ct)
            ? NoContent() : NotFound(new { message = "Không tìm thấy sản phẩm." });
}

public class StockRequest
{
    [Range(0, int.MaxValue)]
    public int StockQuantity { get; set; }
}
