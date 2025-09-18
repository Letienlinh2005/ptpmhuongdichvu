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
    }
}
