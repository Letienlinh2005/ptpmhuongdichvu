using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MyWebAPI.Models;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SachController : ControllerBase
    {
        private readonly string _connStr;
        public SachController(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection")!;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = new List<SachVM>();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            var cmd = new SqlCommand(
                "SELECT MaSach, TieuDe, TacGia, NamXuatBan, MaTheLoai FROM Sach", con);
            using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new SachVM
                {
                    MaSach = rd.GetString(0),
                    TenSach = rd.GetString(1),
                    TacGia = rd.GetString(2),
                    NamXuatBan = rd.IsDBNull(3) ? null : rd.GetInt32(3),
                    TheLoai = rd.IsDBNull(4) ? null : rd.GetString(4)
                });
            }
            return Ok(list);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            var cmd = new SqlCommand(
                "SELECT MaSach, TieuDe, TacGia, NamXuatBan, MaTheLoai FROM Sach WHERE MaSach=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                var sach = new SachVM
                {
                    MaSach = rd.GetString(0),
                    TenSach = rd.GetString(1),
                    TacGia = rd.GetString(2),
                    NamXuatBan = rd.IsDBNull(3) ? null : rd.GetInt32(3),
                    TheLoai = rd.IsDBNull(4) ? null : rd.GetString(4)
                };
                return Ok(sach);
            }
            return NotFound(new { message = "Không tìm thấy sách" });
        }

        [HttpPost]
        public async Task<IActionResult> Create(SachVM input)
        {
            var newId = input.MaSach ?? "S" + Guid.NewGuid().ToString("N")[..7].ToUpper();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            var cmd = new SqlCommand(
                @"INSERT INTO Sach (MaSach, TieuDe, TacGia, NamXuatBan, MaTheLoai)
                  VALUES (@id,@t,@tg,@n,@tl)", con);
            cmd.Parameters.AddWithValue("@id", newId);
            cmd.Parameters.AddWithValue("@t", input.TenSach);
            cmd.Parameters.AddWithValue("@tg", input.TacGia);
            cmd.Parameters.AddWithValue("@n", (object?)input.NamXuatBan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tl", (object?)input.TheLoai ?? DBNull.Value);
            var rows = await cmd.ExecuteNonQueryAsync();
            if (rows > 0)
            {
                input.MaSach = newId;
                return Ok(new { message = "Thêm thành công", data = input });
            }
            return StatusCode(500, new { message = "Không thêm được" });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, SachVM input)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            var cmd = new SqlCommand(
                @"UPDATE Sach SET TieuDe=@t, TacGia=@tg, NamXuatBan=@n, MaTheLoai=@tl
                  WHERE MaSach=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            cmd.Parameters.AddWithValue("@t", input.TenSach);
            cmd.Parameters.AddWithValue("@tg", input.TacGia);
            cmd.Parameters.AddWithValue("@n", (object?)input.NamXuatBan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@tl", (object?)input.TheLoai ?? DBNull.Value);
            var rows = await cmd.ExecuteNonQueryAsync();
            if (rows > 0) return Ok(new { message = "Cập nhật thành công" });
            return NotFound(new { message = "Không tìm thấy sách" });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            var cmd = new SqlCommand("DELETE FROM Sach WHERE MaSach=@id", con);
            cmd.Parameters.AddWithValue("@id", id);
            var rows = await cmd.ExecuteNonQueryAsync();
            if (rows > 0) return Ok(new { message = "Xoá thành công" });
            return NotFound(new { message = "Không tìm thấy sách" });
        }
    }
}
