using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanSaoController : ControllerBase
    {
        private readonly string _connStr;
        public BanSaoController(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection")!;
        }
        //Get api/BanSao
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = new List<Models.BanSao>();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetAllBanDoc", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new Models.BanSao
                {
                    maBanSao = rd.GetString(0),
                    maVach = rd.GetString(1),
                    maSach = rd.GetString(2),
                    maKe = rd.GetString(3),
                    trangThai = rd.GetString(4)
                });
            }
            return Ok(list);
        }
        // GET api/BanSao/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetBanSaoById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaBanSao", id);

            using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                var sach = new Models.BanSao
                {
                    maBanSao = rd.GetString(0),
                    maVach = rd.GetString(1),
                    maSach = rd.GetString(2),
                    maKe = rd.GetString(3),
                    trangThai = rd.GetString(4)
                };
                return Ok(sach);
            }
            return NotFound(new { message = "Không tìm thấy bản sao" });
        }

        // POST api/BanSao
        [HttpPost]
        public async Task<IActionResult> Create(Models.BanSao input)
        {
            var newId = input.maBanSao ?? "BS" + Guid.NewGuid().ToString("N")[..7].ToUpper();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_InsertBanSao", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MaBanSao", newId);
            cmd.Parameters.AddWithValue("@MaVach", input.maVach);
            cmd.Parameters.AddWithValue("@MaSach", input.maSach);
            cmd.Parameters.AddWithValue("@MaKe", (object?)input.maKe ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TrangThai", (object?)input.trangThai ?? DBNull.Value);

            try
            {
                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
                    input.maBanSao = newId;
                    return Ok(new { message = "Thêm thành công", data = input });
                }
                return StatusCode(500, new { message = "Không thêm được" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // PUT api/BanSao/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] Models.BanSao input)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_UpdateBanSao", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MaBanSao", id);
            cmd.Parameters.AddWithValue("@MaVach", input.maVach);
            cmd.Parameters.AddWithValue("@MaSach", input.maSach);
            cmd.Parameters.AddWithValue("@MaKe", (object?)input.maKe ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@TrangThai", (object?)input.trangThai ?? DBNull.Value);

            var rows = await cmd.ExecuteNonQueryAsync();
            if (rows > 0) return Ok(new { message = "Cập nhật thành công" });
            return NotFound(new { message = "Không tìm thấy bản sao" });
        }
        // DELETE api/BanSao/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_DeleteBanSao", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaBanSao", id);

            var rows = await cmd.ExecuteNonQueryAsync();
            if (rows > 0) return Ok(new { message = "Xoá thành công" });
            return NotFound(new { message = "Không tìm thấy bản sao" });
        }
    }
}
