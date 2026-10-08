using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using MiniSupermarket.API.Data;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    [Authorize(Roles = "Admin,Cashier,Warehouse")]
    public class ProductsController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        public ProductsController(SupermarketDbContext context)
        {
            _context = context;
        }

        [HttpGet("barcode/{barcode}")]
        public async Task<IActionResult> GetByBarcode(string barcode)
        {
            barcode = barcode.Trim();

            var product = await _context.Products
                .AsNoTracking()
                .Where(product => product.Barcode == barcode)
                .Select(product => new
                {
                    product.ProductId,
                    product.Barcode,
                    product.ProductName,
                    product.Price,
                    product.StockQuantity
                })
                .FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm có mã vạch này!"
                });
            }

            return Ok(product);
        }
    }
}