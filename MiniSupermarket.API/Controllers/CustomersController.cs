using MiniSupermarket.API.DTOs;
using MiniSupermarket.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;

        // Tiêm DbContext thông qua Constructor Injection
        public CustomersController(SupermarketDbContext context)
        {
            _context = context;
        }

        // Bài 6.1: điểm giảm dần, khóa chính giải quyết trường hợp bằng điểm.
        [HttpGet("paged")]
        public async Task<ActionResult<PagedResult<Customer>>> GetPagedCustomers(
            [FromQuery] string? keyword, [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 5, CancellationToken cancellationToken = default)
        {
            var query = _context.Customers.AsNoTracking();
            if (!string.IsNullOrWhiteSpace(keyword))
            {
                string search = keyword.Trim().ToLower();
                query = query.Where(c => c.CustomerName.ToLower().Contains(search)
                    || c.PhoneNumber.Contains(search));
            }
            int total = await query.CountAsync(cancellationToken);
            var (page, size, pages) = Paging.Normalize(pageNumber, pageSize, total);
            var items = await query.OrderByDescending(c => c.RewardPoints).ThenBy(c => c.CustomerId)
                .Skip((page - 1) * size).Take(size).ToListAsync(cancellationToken);
            return Ok(new PagedResult<Customer>
            { CurrentPage = page, PageSize = size, TotalItems = total, TotalPages = pages, Items = items });
        }

        // 1. READ: Lấy toàn bộ danh sách khách hàng
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var customers = await _context.Customers
                .AsNoTracking()
                .ToListAsync();

            return Ok(customers);
        }

        // 2. READ: Lấy chi tiết khách hàng theo ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng trong hệ thống!"
                });
            }

            return Ok(customer);
        }

        // 3. SEARCH: Tìm khách hàng theo tên hoặc số điện thoại
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                return BadRequest(new
                {
                    message = "Vui lòng nhập từ khóa tìm kiếm!"
                });
            }

            var result = await _context.Customers
                .AsNoTracking()
                .Where(c =>
                    c.CustomerName.Contains(keyword) ||
                    c.PhoneNumber.Contains(keyword))
                .ToListAsync();

            return Ok(result);
        }

        // 4. CREATE: Thêm mới khách hàng thành viên
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Customer newCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Kiểm tra số điện thoại đã được sử dụng hay chưa
            var phoneExists = await _context.Customers
                .AnyAsync(c => c.PhoneNumber == newCustomer.PhoneNumber);

            if (phoneExists)
            {
                return BadRequest(new
                {
                    message = "Số điện thoại này đã được đăng ký!"
                });
            }

            _context.Customers.Add(newCustomer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetById),
                new { id = newCustomer.CustomerId },
                newCustomer
            );
        }

        // 5. UPDATE: Cập nhật thông tin và hạng thẻ khách hàng
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(
            int id,
            [FromBody] Customer updateCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng cần cập nhật!"
                });
            }

            // Không cho phép trùng số điện thoại với khách hàng khác
            var phoneExists = await _context.Customers
                .AnyAsync(c =>
                    c.PhoneNumber == updateCustomer.PhoneNumber &&
                    c.CustomerId != id);

            if (phoneExists)
            {
                return BadRequest(new
                {
                    message = "Số điện thoại này đã được khách hàng khác sử dụng!"
                });
            }

            customer.CustomerName = updateCustomer.CustomerName;
            customer.PhoneNumber = updateCustomer.PhoneNumber;
            customer.Address = updateCustomer.Address;
            customer.RewardPoints = updateCustomer.RewardPoints;
            customer.MembershipRank = updateCustomer.MembershipRank;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // 6. DELETE: Xóa khách hàng khỏi hệ thống
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _context.Customers.FindAsync(id);

            if (customer == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy khách hàng cần xóa!"
                });
            }

            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
