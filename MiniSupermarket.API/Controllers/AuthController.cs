using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

using MiniSupermarket.API.Data;
using MiniSupermarket.API.Models;

namespace MiniSupermarket.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly SupermarketDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthController(
            IConfiguration configuration,
            SupermarketDbContext context,
            IPasswordHasher<User> passwordHasher)
        {
            _configuration = configuration;
            _context = context;
            _passwordHasher = passwordHasher;
        }

        // POST /api/auth/login
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login(
            [FromBody] LoginRequestDto request)
        {
            string username = request.Username.Trim();

            if (string.IsNullOrWhiteSpace(username))
            {
                return BadRequest(new
                {
                    success = false,
                    message = "Tên đăng nhập không được để trống!"
                });
            }

            var user = await _context.Users
                .SingleOrDefaultAsync(
                    account => account.Username == username);

            if (user == null || !user.IsActive)
            {
                return LoginFailed();
            }

            var verificationResult =
                _passwordHasher.VerifyHashedPassword(
                    user,
                    user.PasswordHash,
                    request.Password);

            if (verificationResult == PasswordVerificationResult.Failed)
            {
                return LoginFailed();
            }

            // Chỉ cấp token cho ba vai trò được hỗ trợ.
            string? role = NormalizeRole(user.Role);

            if (role == null)
            {
                return StatusCode(StatusCodes.Status403Forbidden, new
                {
                    success = false,
                    message = "Tài khoản chưa được cấp vai trò hợp lệ!"
                });
            }

            if (verificationResult ==
                PasswordVerificationResult.SuccessRehashNeeded)
            {
                user.PasswordHash =
                    _passwordHasher.HashPassword(user, request.Password);

                await _context.SaveChangesAsync();
            }

            string token = GenerateJwtToken(user, role);

            // Giữ các trường token và role để WinForms tiếp tục sử dụng.
            return Ok(new
            {
                success = true,
                token,
                role,
                username = user.Username,
                fullName = user.FullName
            });
        }

        private UnauthorizedObjectResult LoginFailed()
        {
            return Unauthorized(new
            {
                success = false,
                message =
                    "Sai tài khoản, mật khẩu hoặc tài khoản đã bị khóa!"
            });
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

        private string GenerateJwtToken(User user, string role)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            // Giữ cùng khóa và cách mã hóa với Program.cs hiện tại.
            string secret = _configuration["JwtSettings:Secret"]
                ?? "SupermarketSecretKeyDoAnMonHoc2026SecureString!!";

            byte[] key = Encoding.ASCII.GetBytes(secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(
                        ClaimTypes.NameIdentifier,
                        user.Id.ToString()),

                    new Claim(
                        ClaimTypes.Name,
                        user.Username),

                    new Claim(
                        ClaimTypes.Role,
                        role)
                }),

                Expires = DateTime.UtcNow.AddHours(2),

                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }

    public class LoginRequestDto
    {
        [Required]
        [StringLength(50)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(200)]
        public string Password { get; set; } = string.Empty;
    }
}