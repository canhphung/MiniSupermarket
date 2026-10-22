using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

using MiniSupermarket.API.Models;
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

        // Buổi 6: lọc trước Count; SQL chỉ trả về một trang.
        [HttpGet("paged")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<ActionResult<PagedResult<ProductItemDto>>> GetPagedProducts(
            [FromQuery] string? keyword, [FromQuery] int? categoryId,
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 5,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Products.Include(p => p.Category).AsNoTracking().AsQueryable();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(p => p.ProductName.ToLower().Contains(search)
                    || p.Barcode.Contains(search));
            }
            if (categoryId.HasValue && categoryId.Value > 0)
                query = query.Where(p => p.CategoryId == categoryId.Value);

            int total = await query.CountAsync(cancellationToken);
            var (page, size, pages) = Paging.Normalize(pageNumber, pageSize, total);
            // Project CategoryName thành JOIN; không truy vấn riêng cho từng dòng (N+1).
            var items = await query.OrderBy(p => p.ProductId)
                .Skip((page - 1) * size).Take(size)
                .Select(p => new ProductItemDto
                {
                    ProductId = p.ProductId, Barcode = p.Barcode, ProductName = p.ProductName,
                    Price = p.Price, StockQuantity = p.StockQuantity, CategoryId = p.CategoryId,
                    CategoryName = p.Category != null ? p.Category.CategoryName : string.Empty
                }).ToListAsync(cancellationToken);
            return Ok(new PagedResult<ProductItemDto>
            { CurrentPage = page, PageSize = size, TotalItems = total, TotalPages = pages, Items = items });
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
        // GET /api/products?barcode=893&categoryId=1
        [HttpGet]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? barcode,
            [FromQuery] int? categoryId)
        {
            var query = _context.Products.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(barcode))
            {
                string keyword = barcode.Trim();

                query = query.Where(p => p.Barcode.Contains(keyword));
            }

            if (categoryId.HasValue)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
            }

            var products = await query
                .OrderBy(p => p.ProductId)
                .Select(p => new
                {
                    p.ProductId,
                    p.Barcode,
                    p.ProductName,
                    p.Price,
                    p.StockQuantity,
                    p.CategoryId,
                    CategoryName = p.Category != null
                        ? p.Category.CategoryName
                        : ""
                })
                .ToListAsync();

            return Ok(products);
        }

        // POST /api/products
        [HttpPost]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Create(
            [FromBody] ProductSaveRequest request)
        {
            string barcode = request.Barcode.Trim();
            string name = request.ProductName.Trim();

            if (barcode.Length == 0 || name.Length == 0)
            {
                return BadRequest(new
                {
                    message = "Mã vạch và tên sản phẩm không được trống!"
                });
            }

            if (!await _context.Categories
                .AnyAsync(c => c.CategoryId == request.CategoryId))
            {
                return BadRequest(new
                {
                    message = "Danh mục không tồn tại!"
                });
            }

            if (await _context.Products.AnyAsync(p => p.Barcode == barcode))
            {
                return Conflict(new
                {
                    message = "Mã vạch đã được sử dụng!"
                });
            }

            var product = new Product
            {
                Barcode = barcode,
                ProductName = name,
                Price = request.Price,
                StockQuantity = request.StockQuantity,
                CategoryId = request.CategoryId
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return StatusCode(StatusCodes.Status201Created, new
            {
                product.ProductId,
                message = "Thêm sản phẩm thành công!"
            });
        }

        // PUT /api/products/1
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] ProductSaveRequest request)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm!"
                });
            }

            string barcode = request.Barcode.Trim();
            string name = request.ProductName.Trim();

            if (barcode.Length == 0 || name.Length == 0)
            {
                return BadRequest(new
                {
                    message = "Mã vạch và tên sản phẩm không được trống!"
                });
            }

            if (!await _context.Categories
                .AnyAsync(c => c.CategoryId == request.CategoryId))
            {
                return BadRequest(new
                {
                    message = "Danh mục không tồn tại!"
                });
            }

            if (await _context.Products.AnyAsync(
                p => p.Barcode == barcode && p.ProductId != id))
            {
                return Conflict(new
                {
                    message = "Mã vạch đã được sản phẩm khác sử dụng!"
                });
            }

            product.Barcode = barcode;
            product.ProductName = name;
            product.Price = request.Price;
            product.StockQuantity = request.StockQuantity;
            product.CategoryId = request.CategoryId;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE /api/products/1
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin,Warehouse")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy sản phẩm!"
                });
            }

            if (await _context.OrderItems.AnyAsync(i => i.ProductId == id))
            {
                return Conflict(new
                {
                    message = "Sản phẩm đã có trong hóa đơn, không thể xóa!"
                });
            }

            _context.Products.Remove(product);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is Microsoft.Data.SqlClient.SqlException sql
                    && sql.Number == 547)
            {
                return Conflict(new
                {
                    message = "Sản phẩm đang được dữ liệu khác tham chiếu, không thể xóa!"
                });
            }

            return NoContent();
        }
    }
    public class ProductSaveRequest
    {
        [Required(ErrorMessage = "Mã vạch không được để trống!")]
        [StringLength(
            50,
            ErrorMessage = "Mã vạch không được vượt quá 50 ký tự!")]
        public string Barcode { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tên sản phẩm không được để trống!")]
        [StringLength(
            150,
            ErrorMessage = "Tên sản phẩm không được vượt quá 150 ký tự!")]
        public string ProductName { get; set; } = string.Empty;

        [Range(
            typeof(decimal),
            "0",
            "9999999999999999.99",
            ParseLimitsInInvariantCulture = true,
            ConvertValueInInvariantCulture = true,
            ErrorMessage = "Giá bán nằm ngoài phạm vi cho phép!")]
        public decimal Price { get; set; }

        [Range(
            0,
            int.MaxValue,
            ErrorMessage = "Số lượng tồn kho không được âm!")]
        public int StockQuantity { get; set; }

        [Range(
            1,
            int.MaxValue,
            ErrorMessage = "Vui lòng chọn nhóm hàng hợp lệ!")]
        public int CategoryId { get; set; }
    }
}
