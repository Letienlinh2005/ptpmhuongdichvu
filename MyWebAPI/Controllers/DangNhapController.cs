using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Text.Json.Serialization;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DangNhapController : ControllerBase
    {
        private readonly string _connStr;
        public DangNhapController(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection")!;
        }


        public record LoginDto(
            [property: JsonPropertyName("tenDangNhap")] string TenDangNhap,
            [property: JsonPropertyName("matKhau")] string MatKhau
        );

        [HttpPost("login")]
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

            // Lấy hash từ DB để verify
            var hashCol = rd.GetOrdinal("MatKhau");
            var hash = rd.IsDBNull(hashCol) ? "" : rd.GetString(hashCol);

            var ok = BCrypt.Net.BCrypt.Verify(req.MatKhau, hash);
            if (!ok) return Unauthorized(new { message = "Sai thông tin đăng nhập" });

            // Lấy thông tin trả về (không bao giờ trả password)
            var id = rd.GetString(rd.GetOrdinal("MaTaiKhoan"));
            var role = rd.GetString(rd.GetOrdinal("VaiTro"));

            // TODO: nếu bạn dùng JWT, tạo token tại đây rồi trả về
            return Ok(new
            {
                maTaiKhoan = id,
                tenDangNhap = req.TenDangNhap,
                vaiTro = role
                // accessToken = ..., refreshToken = ...
            });
        }
    }
}
