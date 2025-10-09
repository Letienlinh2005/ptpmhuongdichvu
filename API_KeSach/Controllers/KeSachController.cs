using API_KeSach.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_KeSach.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KeSachController : ControllerBase
    {
        private readonly string _connStr;
        public KeSachController(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection")!;
        }

        // GET api/kesach
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = new List<Models.KeSach>();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetAllKeSach", con);
            cmd.CommandType = CommandType.StoredProcedure;
            using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new Models.KeSach
                {
                    maKe = rd.GetString(0),
                    viTri = rd.GetString(1)
                });
            }
            return Ok(list);
        }
        // GET api/kesach/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Models.KeSach? phieumuon = null;
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetKeSachById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaKeSach", id);
            using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                phieumuon = new Models.KeSach
                {
                    maKe = rd.GetString(0),
                    viTri = rd.GetString(1)
                };
            }
            if (phieumuon == null)
                return NotFound(new { message = "Không tìm thấy kệ sách" });
            return Ok(phieumuon);
        }
        // POST api/KeSach
        [HttpPost]
        public async Task<IActionResult> Create(Models.KeSach kesach)
        {
            var newId = kesach.maKe ?? "BD" + Guid.NewGuid().ToString("N")[..7].ToUpper();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_CreatePhieuMuon", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaKe", newId);
            cmd.Parameters.AddWithValue("@MaKe", kesach.maKe);
            cmd.Parameters.AddWithValue("@ViTri", kesach.viTri);
            try
            {
                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
                    kesach.maKe = newId;
                    return Ok(new { message = "Thêm thành công", data = kesach });
                }
                return StatusCode(500, new { message = "Không thêm được" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        // PUT api/Kesach/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Models.KeSach kesach)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_UpdatePhieuMuon", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaKe", id);
            cmd.Parameters.AddWithValue("@ViTri", kesach.viTri);
            try
            {
                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
                    return Ok(new { message = "Cập nhật thành công", data = kesach });
                }
                return NotFound(new { message = "Không tìm thấy phiếu mượn" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        // DELETE api/kesach/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_DeleteKeSach", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaSach", id);

            var rows = await cmd.ExecuteNonQueryAsync();
            if (rows > 0) return Ok(new { message = "Xoá thành công" });
            return NotFound(new { message = "Không tìm thấy sách" });
        }
    }
}
