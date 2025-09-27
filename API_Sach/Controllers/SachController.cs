using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

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

        // GET api/sach
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = new List<Models.SachVM>();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetAllSach", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new Models.SachVM
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

        // GET api/sach/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetSachById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaSach", id);

            using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                var sach = new Models.SachVM
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

        // POST api/sach
        [HttpPost]
        public async Task<IActionResult> Create(Models.SachVM input)
        {
            var newId = input.MaSach ?? "S" + Guid.NewGuid().ToString("N")[..7].ToUpper();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_InsertSach", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MaSach", newId);
            cmd.Parameters.AddWithValue("@TieuDe", input.TenSach);
            cmd.Parameters.AddWithValue("@TacGia", input.TacGia);
            cmd.Parameters.AddWithValue("@NamXuatBan", (object?)input.NamXuatBan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaTheLoai", (object?)input.TheLoai ?? DBNull.Value);

            try
            {
                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
                    input.MaSach = newId;
                    return Ok(new { message = "Thêm thành công", data = input });
                }
                return StatusCode(500, new { message = "Không thêm được" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/sach/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Models.SachVM input)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_UpdateSach", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MaSach", id);
            cmd.Parameters.AddWithValue("@TieuDe", input.TenSach);
            cmd.Parameters.AddWithValue("@TacGia", input.TacGia);
            cmd.Parameters.AddWithValue("@NamXuatBan", (object?)input.NamXuatBan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaTheLoai", (object?)input.TheLoai ?? DBNull.Value);

            var rows = await cmd.ExecuteNonQueryAsync();
            if (rows > 0) return Ok(new { message = "Cập nhật thành công" });
            return NotFound(new { message = "Không tìm thấy sách" });
        }

        // DELETE api/sach/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_DeleteSach", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaSach", id);

            var rows = await cmd.ExecuteNonQueryAsync();
            if (rows > 0) return Ok(new { message = "Xoá thành công" });
            return NotFound(new { message = "Không tìm thấy sách" });
        }
    }
}
