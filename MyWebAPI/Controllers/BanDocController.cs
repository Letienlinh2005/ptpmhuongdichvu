using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Diagnostics.Metrics;
using System.Net.WebSockets;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanDocController : ControllerBase
    {
        private readonly string _connStr;
        public BanDocController(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection")!;
        }

        // GET api/bandoc
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = new List<Models.BanDoc>();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetAllBanDoc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new Models.BanDoc
                {
                    maBanDoc = rd.GetString(0),
                    hoTen = rd.GetString(1),
                    soThe = rd.GetString(2),
                    email = rd.GetString(3),
                    dienThoai = rd.GetString(4),
                    hanThe = DateOnly.FromDateTime(rd.GetDateTime(5)),
                    trangThaiThe = rd.GetString(6),
                    duNo = rd.GetDecimal(7)
                });
            }
            return Ok(list);
        }

        // GET api/bandoc/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Models.BanDoc? banDoc = null;
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetBanDocById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaBanDoc", id);
            using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                banDoc = new Models.BanDoc
                {
                    maBanDoc = rd.GetString(0),
                    hoTen = rd.GetString(1),
                    soThe = rd.GetString(2),
                    email = rd.GetString(3),
                    dienThoai = rd.GetString(4),
                    hanThe = DateOnly.FromDateTime(rd.GetDateTime(5)),
                    trangThaiThe = rd.GetString(6),
                    duNo = rd.GetDecimal(7)
                };
            }
            if (banDoc == null)
                return NotFound(new { message = "Không tìm thấy bạn đọc" });
            return Ok(banDoc);
        }

        // POST api/bandoc
        [HttpPost]
        public async Task<IActionResult> Create(Models.BanDoc banDoc)
        {
            var newId = banDoc.maBanDoc ?? "BD" + Guid.NewGuid().ToString("N")[..7].ToUpper();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_CreateBanDoc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaBanDoc", newId);
            cmd.Parameters.AddWithValue("@HoTen", banDoc.hoTen);
            cmd.Parameters.AddWithValue("@SoThe", banDoc.soThe);
            cmd.Parameters.AddWithValue("@Email", banDoc.email);
            cmd.Parameters.AddWithValue("@DienThoai", banDoc.dienThoai);
            cmd.Parameters.AddWithValue("@HanThe", banDoc.hanThe.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@TrangThaiThe", banDoc.trangThaiThe);
            cmd.Parameters.AddWithValue("@DuNo", banDoc.duNo);
            try
            {
                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
                    banDoc.maBanDoc = newId;
                    return Ok(new { message = "Thêm thành công", data = banDoc });
                }
                return StatusCode(500, new { message = "Không thêm được" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }

        // PUT api/bandoc/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Models.BanDoc banDoc)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_UpdateBanDoc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaBanDoc", id);
            cmd.Parameters.AddWithValue("@HoTen", banDoc.hoTen);
            cmd.Parameters.AddWithValue("@SoThe", banDoc.soThe);
            cmd.Parameters.AddWithValue("@Email", banDoc.email);
            cmd.Parameters.AddWithValue("@DienThoai", banDoc.dienThoai);
            cmd.Parameters.AddWithValue("@HanThe", banDoc.hanThe.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@TrangThaiThe", banDoc.trangThaiThe);
            cmd.Parameters.AddWithValue("@DuNo", banDoc.duNo);
            try
            {
                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
                    return Ok(new { message = "Cập nhật thành công", data = banDoc });
                }
                return NotFound(new { message = "Không tìm thấy bạn đọc" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // DELETE api/bandoc/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_DeleteBanDoc", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaBanDoc", id);
            try
            {
                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
                    return Ok(new { message = "Xóa thành công" });
                }
                return NotFound(new { message = "Không tìm thấy bạn đọc" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        
    }
}
