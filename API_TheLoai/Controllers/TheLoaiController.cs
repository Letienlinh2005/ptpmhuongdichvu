using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_TheLoai.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TheLoaiController : ControllerBase
    {
        private readonly string _connStr;
        public TheLoaiController(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection")!;
        }

        // GET api/theloai
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = new List<Models.TheLoaiModels>();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetAllTheLoai", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new Models.TheLoaiModels
                {
                    maTheLoai = rd.GetString(0),
                    tenTheLoai = rd.GetString(1),
                    moTa = rd.GetString(2)
                });
            }
            return Ok(list);
        }

        // GET api/theloai/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetTheloaiById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaTheLoai", id);

            using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                var sach = new Models.TheLoaiModels
                {
                    maTheLoai = rd.GetString(0),
                    tenTheLoai = rd.GetString(1),
                    moTa = rd.GetString(2)
                };
                return Ok(sach);
            }
            return NotFound(new { message = "Không tìm thấy thể loại" });
        }

        // POST api/theloai
        [HttpPost]
        public async Task<IActionResult> Create(Models.TheLoaiModels input)
        {
            var newId = input.maTheLoai ?? "S" + Guid.NewGuid().ToString("N")[..7].ToUpper();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_InsertTheLoai", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MaTheLoai", newId);
            cmd.Parameters.AddWithValue("@TenTheLoai", input.tenTheLoai);
            cmd.Parameters.AddWithValue("@MoTa", input.moTa);
            try
            {
                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
                    input.maTheLoai = newId;
                    return Ok(new { message = "Thêm thành công", data = input });
                }
                return StatusCode(500, new { message = "Không thêm được" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // PUT api/theloai/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Models.TheLoaiModels input)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_UpdateTheLoai", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MaTheLoai", id);
            cmd.Parameters.AddWithValue("@TenTheLoai", input.tenTheLoai);
            cmd.Parameters.AddWithValue("@MoTa", input.moTa);

            var rows = await cmd.ExecuteNonQueryAsync();
            if (rows > 0) return Ok(new { message = "Cập nhật thành công" });
            return NotFound(new { message = "Không tìm thấy thể loại" });
        }

        // DELETE api/sach/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_DeleteTheLoai", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaTheLoai", id);

            var rows = await cmd.ExecuteNonQueryAsync();
            if (rows > 0) return Ok(new { message = "Xoá thành công" });
            return NotFound(new { message = "Không tìm thấy thể loại" });
        }
    }
}
