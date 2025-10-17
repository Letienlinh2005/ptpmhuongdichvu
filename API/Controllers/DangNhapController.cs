using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DangNhapController : ControllerBase
    {
        private readonly string _connStr;
        private readonly IConfiguration _config;

        public DangNhapController(IConfiguration config)
        {
            _config = config;
            _connStr = config.GetConnectionString("DefaultConnection")!;
        }

        public record LoginDto(
            [property: JsonPropertyName("tenDangNhap")] string TenDangNhap,
            [property: JsonPropertyName("matKhau")] string MatKhau
        );

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginDto req)
        {
            if (string.IsNullOrWhiteSpace(req.TenDangNhap) || string.IsNullOrWhiteSpace(req.MatKhau))
                return BadRequest(new { message = "Thiếu tên đăng nhập hoặc mật khẩu" });

            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();

            using var cmd = new SqlCommand("sp_Login", con) { CommandType = CommandType.StoredProcedure };
            cmd.Parameters.Add("@TenDangNhap", SqlDbType.NVarChar, 100).Value = req.TenDangNhap;

            using var rd = await cmd.ExecuteReaderAsync();
            if (!await rd.ReadAsync())
                return Unauthorized(new { message = "Sai thông tin đăng nhập" });

            var hashCol = rd.GetOrdinal("MatKhau");
            var hash = rd.IsDBNull(hashCol) ? "" : rd.GetString(hashCol);
            if (!BCrypt.Net.BCrypt.Verify(req.MatKhau, hash))
                return Unauthorized(new { message = "Sai thông tin đăng nhập" });

            var id = rd.GetString(rd.GetOrdinal("MaTaiKhoan"));
            var role = rd.GetString(rd.GetOrdinal("VaiTro"));

            var accessToken = GenerateAccessToken(id, req.TenDangNhap, role);

            return Ok(new
            {
                message = "Đăng nhập thành công",
                maTaiKhoan = id,
                tenDangNhap = req.TenDangNhap,
                vaiTro = role,
                accessToken
            });
        }

        // Ví dụ 1 API cần token
        [Authorize]
        [HttpGet("me")]
        public IActionResult Me()
        {
            var uid = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var name = User.Identity?.Name;
            var roles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);
            return Ok(new { uid, name, roles });
        }

        private string GenerateAccessToken(string userId, string username, string role)
        {
            var jwt = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId),
                new Claim(JwtRegisteredClaimNames.UniqueName, username),
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, role)
            };

            var expires = DateTime.UtcNow.AddMinutes(double.Parse(jwt["AccessTokenMinutes"]!));

            var token = new JwtSecurityToken(
                issuer: jwt["Issuer"],
                audience: jwt["Audience"],
                claims: claims,
                notBefore: DateTime.UtcNow,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
