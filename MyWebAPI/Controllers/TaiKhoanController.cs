using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;


namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaiKhoanController : ControllerBase
    {
        private readonly string _connStr;
        public TaiKhoanController(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection")!;
        }

        // GET api/taikhoan
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = new List<Models.TaiKhoan>();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetAllTaiKhoan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new Models.TaiKhoan
                {
                    maTaiKhoan = rd.GetString(0),
                    tenDangNhap = rd.GetString(1),
                    matKhau = rd.GetString(2),
                    vaiTro = rd.GetString(3)
                });
            }
            return Ok(list);
        }

        // GET api/taikhoan/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
            {
            Models.TaiKhoan? taiKhoan = null;
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetTaiKhoanById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaTaiKhoan", id);
            using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                taiKhoan = new Models.TaiKhoan
                {
                    maTaiKhoan = rd.GetString(0),
                    tenDangNhap = rd.GetString(1),
                    matKhau = rd.GetString(2),
                    vaiTro = rd.GetString(3)
                };
            }
            if (taiKhoan == null)
                return NotFound();
            return Ok(taiKhoan);
        }

        // POST api/taikhoan
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Models.TaiKhoan taiKhoan)
        {
            var newId = taiKhoan.maTaiKhoan ?? "TK" + Guid.NewGuid().ToString("N")[..7].ToUpper();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_Register", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaTaiKhoan", newId);
            cmd.Parameters.AddWithValue("@TenDangNhap", taiKhoan.tenDangNhap);
            cmd.Parameters.AddWithValue("@MatKhau", BCrypt.Net.BCrypt.HashPassword(taiKhoan.matKhau, 10));
            cmd.Parameters.AddWithValue("@VaiTro", taiKhoan.vaiTro);
            
            try
            {
                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
                    taiKhoan.maTaiKhoan = newId;
                    return Ok(new { message = "Thêm thành công", data = taiKhoan });
                }
                return StatusCode(500, new { message = "Không thêm được" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        // PUT api/taikhoan/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Models.TaiKhoan taiKhoan)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_UpdateTaiKhoan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaTaiKhoan", id);
            cmd.Parameters.AddWithValue("@TenDangNhap", taiKhoan.tenDangNhap);
            cmd.Parameters.AddWithValue("@MatKhau", BCrypt.Net.BCrypt.HashPassword(taiKhoan.matKhau, 10));
            cmd.Parameters.AddWithValue("@VaiTro", taiKhoan.vaiTro);
            var rowsAffected = await cmd.ExecuteNonQueryAsync();
            if (rowsAffected > 0)
                return NoContent();
            else
                return NotFound();
        }

        // DELETE api/taikhoan/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_DeleteTaiKhoan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaTaiKhoan", id);
            var rowsAffected = await cmd.ExecuteNonQueryAsync();
            if (rowsAffected > 0)
                return NoContent();
            else
                return NotFound();
        }
    }
}
