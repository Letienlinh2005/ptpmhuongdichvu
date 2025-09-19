using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;


namespace MyWebAPI.Controllers
{
    [Authorize]
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
                    tenTaiKhoan = rd.GetString(1),
                    matKhau = rd.GetString(2),
                    vaiTro = rd.GetString(3),
                    maBanDoc = rd.IsDBNull(4) ? null : rd.GetString(4)
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
                    tenTaiKhoan = rd.GetString(1),
                    matKhau = rd.GetString(2),
                    vaiTro = rd.GetString(3),
                    maBanDoc = rd.IsDBNull(4) ? null : rd.GetString(4)
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
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_CreateTaiKhoan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaTaiKhoan", taiKhoan.maTaiKhoan);
            cmd.Parameters.AddWithValue("@TenTaiKhoan", taiKhoan.tenTaiKhoan);
            cmd.Parameters.AddWithValue("@MatKhau", taiKhoan.matKhau);
            cmd.Parameters.AddWithValue("@VaiTro", taiKhoan.vaiTro);
            if (taiKhoan.maBanDoc != null)
                cmd.Parameters.AddWithValue("@MaBanDoc", taiKhoan.maBanDoc);
            else
                cmd.Parameters.AddWithValue("@MaBanDoc", DBNull.Value);
            var rowsAffected = await cmd.ExecuteNonQueryAsync();
            if (rowsAffected > 0)
                return CreatedAtAction(nameof(GetById), new { id = taiKhoan.maTaiKhoan }, taiKhoan);
            else
                return BadRequest();
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
            cmd.Parameters.AddWithValue("@TenTaiKhoan", taiKhoan.tenTaiKhoan);
            cmd.Parameters.AddWithValue("@MatKhau", taiKhoan.matKhau);
            cmd.Parameters.AddWithValue("@VaiTro", taiKhoan.vaiTro);
            if (taiKhoan.maBanDoc != null)
                cmd.Parameters.AddWithValue("@MaBanDoc", taiKhoan.maBanDoc);
            else
                cmd.Parameters.AddWithValue("@MaBanDoc", DBNull.Value);
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
