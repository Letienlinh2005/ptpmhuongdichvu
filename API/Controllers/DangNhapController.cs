using Microsoft.AspNetCore.Mvc;
using MyWebAPI.BLL.Services;
using MyWebAPI.DTO;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
namespace MyWebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DangNhapController : ControllerBase
    {
        private readonly ITaiKhoanService _taiKhoanService;
        private readonly IConfiguration _config;

        public DangNhapController(ITaiKhoanService taiKhoanService, IConfiguration config)
        {
            _taiKhoanService = taiKhoanService;
            _config = config;
        }

        // POST: api/DangNhap/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto req)
        {
            if (req == null || string.IsNullOrWhiteSpace(req.TenDangNhap) || string.IsNullOrWhiteSpace(req.MatKhau))
                return BadRequest("Thiếu thông tin đăng nhập");

            // gọi BLL để nó tự gọi SP + verify bcrypt
            var result = await _taiKhoanService.DangNhapAsync(req.TenDangNhap, req.MatKhau);

            if (!result.Success || result.Data == null)
                return Unauthorized(result);

            var user = result.Data;

            // tạo JWT
            var token = GenerateAccessToken(user.MaTaiKhoan, user.TenDangNhap, user.VaiTro);

            return Ok(new ResponseDTO<object>
            {
                Success = true,
                Message = "Đăng nhập thành công",
                Data = new
                {
                    token,
                    user
                }
            });
        }

        private string GenerateAccessToken(string maTaiKhoan, string tenDangNhap, string vaiTro)
        {
            var key = _config["Jwt:Key"];
            var issuer = _config["Jwt:Issuer"];
            var audience = _config["Jwt:Audience"];

            if (string.IsNullOrWhiteSpace(key))
                throw new InvalidOperationException("Thiếu Jwt:Key trong appsettings.json");

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, maTaiKhoan),
                new Claim("username", tenDangNhap),
                new Claim(ClaimTypes.Role, string.IsNullOrEmpty(vaiTro) ? "User" : vaiTro),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
