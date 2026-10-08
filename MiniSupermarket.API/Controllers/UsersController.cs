using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using MiniSupermarket.API.Data;

using AppUser = MiniSupermarket.API.Models.User;

namespace MiniSupermarket.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly SupermarketDbContext _context;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public UsersController(
            SupermarketDbContext context,
            IPasswordHasher<AppUser> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // GET /api/users
        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetAll()
        {
            var users = await _context.Users
                .AsNoTracking()
                .OrderBy(user => user.Id)
                .Select(user => new UserResponse(
                    user.Id,
                    user.Username,
                    user.FullName,
                    user.Role,
                    user.IsActive))
                .ToListAsync();

            return Ok(users);
        }

        // GET /api/users/16
        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserResponse>> GetById(int id)
        {
            var user = await _context.Users
                .AsNoTracking()
                .Where(user => user.Id == id)
                .Select(user => new UserResponse(
                    user.Id,
                    user.Username,
                    user.FullName,
                    user.Role,
                    user.IsActive))
                .SingleOrDefaultAsync();

            if (user == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy tài khoản!"
                });
            }

            return Ok(user);
        }

        // POST /api/users
        [HttpPost]
        public async Task<ActionResult<UserResponse>> Create(
            [FromBody] CreateUserRequest request)
        {
            string username = request.Username
                .Trim()
                .ToLowerInvariant();

            string? role = NormalizeRole(request.Role);

            if (role == null)
            {
                return BadRequest(new
                {
                    message =
                        "Vai trò phải là Admin, Cashier hoặc Warehouse!"
                });
            }

            bool exists = await _context.Users
                .AnyAsync(user => user.Username == username);

            if (exists)
            {
                return Conflict(new
                {
                    message = "Tên đăng nhập đã tồn tại!"
                });
            }

            var user = new AppUser
            {
                Username = username,
                FullName = request.FullName.Trim(),
                Role = role,
                IsActive = true
            };

            user.PasswordHash = _passwordHasher.HashPassword(
                user,
                request.Password);

            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
                when (ex.InnerException is SqlException sqlException
                    && (sqlException.Number == 2601
                        || sqlException.Number == 2627))
            {
                // Chặn trùng tên nếu có hai request tạo cùng lúc.
                return Conflict(new
                {
                    message = "Tên đăng nhập đã tồn tại!"
                });
            }

            var response = ToResponse(user);

            return CreatedAtAction(
                nameof(GetById),
                new { id = user.Id },
                response);
        }

        // PUT /api/users/16/password
        [HttpPut("{id:int}/password")]
        public async Task<IActionResult> ResetPassword(
            int id,
            [FromBody] ResetPasswordRequest request)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy tài khoản!"
                });
            }

            user.PasswordHash = _passwordHasher.HashPassword(
                user,
                request.NewPassword);

            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH /api/users/16/status
        [HttpPatch("{id:int}/status")]
        public async Task<ActionResult<UserResponse>> SetStatus(
            int id,
            [FromBody] SetUserStatusRequest request)
        {
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound(new
                {
                    message = "Không tìm thấy tài khoản!"
                });
            }

            // Ngăn Admin tự khóa tài khoản đang sử dụng.
            string? currentUserId = User.FindFirstValue(
                ClaimTypes.NameIdentifier);

            if (!request.IsActive!.Value
                && currentUserId == id.ToString())
            {
                return BadRequest(new
                {
                    message =
                        "Bạn không thể tự khóa tài khoản đang đăng nhập!"
                });
            }

            user.IsActive = request.IsActive.Value;

            await _context.SaveChangesAsync();

            return Ok(ToResponse(user));
        }

        private static UserResponse ToResponse(AppUser user)
        {
            return new UserResponse(
                user.Id,
                user.Username,
                user.FullName,
                user.Role,
                user.IsActive);
        }

        private static string? NormalizeRole(string role)
        {
            return role.Trim().ToUpperInvariant() switch
            {
                "ADMIN" => "Admin",
                "CASHIER" => "Cashier",
                "WAREHOUSE" => "Warehouse",
                _ => null
            };
        }
    }

    // ================= REQUEST DTO =================

    public class CreateUserRequest
    {
        [Required(ErrorMessage = "Tên đăng nhập không được trống!")]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required(ErrorMessage = "Họ tên không được trống!")]
        [StringLength(150)]
        public string FullName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Mật khẩu không được trống!")]
        [StringLength(
            200,
            MinimumLength = 6,
            ErrorMessage = "Mật khẩu phải có từ 6 đến 200 ký tự!")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Vai trò không được trống!")]
        [StringLength(20)]
        public string Role { get; set; } = "Cashier";
    }

    public class ResetPasswordRequest
    {
        [Required(ErrorMessage = "Mật khẩu mới không được trống!")]
        [StringLength(
            200,
            MinimumLength = 6,
            ErrorMessage = "Mật khẩu phải có từ 6 đến 200 ký tự!")]
        public string NewPassword { get; set; } = string.Empty;
    }

    public class SetUserStatusRequest
    {
        // Dùng bool? để phát hiện request thiếu IsActive.
        [Required(ErrorMessage = "Phải cung cấp trạng thái IsActive!")]
        public bool? IsActive { get; set; }
    }

    // Không trả PasswordHash về client.
    public record UserResponse(
        int Id,
        string Username,
        string FullName,
        string Role,
        bool IsActive);
}