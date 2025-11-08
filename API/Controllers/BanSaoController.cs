<<<<<<< HEAD


=======
>>>>>>> 2e50a061e88c396942f9de0b63b0b553660fd4f0
﻿using Microsoft.AspNetCore.Mvc;
﻿using Microsoft.AspNetCore.Http;
using Microsoft.Data.SqlClient;
using MyWebAPI.BLL.Services;
using MyWebAPI.DTO;
using System.Data;
using System.Reflection;

namespace MyWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BanSaoController : ControllerBase
    {
        private readonly IBanSaoService _banSaoService;

        public BanSaoController(IBanSaoService banSaoService)
        {
            _banSaoService = banSaoService;
        }

        // GET api/bansao
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var response = await _banSaoService.GetAllAsync();

            if (response.Success)
                return Ok(response);

            return StatusCode(500, response);
        }

        // GET api/bansao/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var response = await _banSaoService.GetByIdAsync(id);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        // POST api/bansao
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBanSaoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _banSaoService.CreateAsync(request);

            if (response.Success)
                return Ok(response);

            return BadRequest(response);
        }

        // PUT api/bansao/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateBanSaoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var response = await _banSaoService.UpdateAsync(id, request);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        // DELETE api/bansao/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _banSaoService.DeleteAsync(id);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }
    }
}
<<<<<<< HEAD
﻿//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using System.Data;
//using Microsoft.Data.SqlClient;


//namespace API_BanSao.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BanSaoController : ControllerBase
//    {
//        private readonly string _connStr;
//        public BanSaoController(IConfiguration config)
//        {
//            _connStr = config.GetConnectionString("DefaultConnection")!;
//        }
//        //Get api/BanSao
//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var list = new List<Models.BanSaoModel>();
//            using var con = new SqlConnection(_connStr);
//            await con.OpenAsync();
//            using var cmd = new SqlCommand("sp_GetAllBanDoc", con);
//            cmd.CommandType = CommandType.StoredProcedure;

//            using var rd = await cmd.ExecuteReaderAsync();
//            while (await rd.ReadAsync())
//            {
//                list.Add(new Models.BanSaoModel
//                {
//                    maBanSao = rd.GetString(0),
//                    maVach = rd.GetString(1),
//                    maSach = rd.GetString(2),
//                    maKe = rd.GetString(3),
//                    trangThai = rd.GetString(4)
//                });
//            }
//            return Ok(list);
//        }
//        // GET api/BanSao/{id}
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(string id)
//        {
//            using var con = new SqlConnection(_connStr);
//            await con.OpenAsync();
//            using var cmd = new SqlCommand("sp_GetBanSaoById", con);
//            cmd.CommandType = CommandType.StoredProcedure;
//            cmd.Parameters.AddWithValue("@MaBanSao", id);

//            using var rd = await cmd.ExecuteReaderAsync();
//            if (await rd.ReadAsync())
//            {
//                var sach = new Models.BanSaoModel

//namespace API_BanSao.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class BanSaoController : ControllerBase
//    {
//        private readonly string _connStr;
//        public BanSaoController(IConfiguration config)
//        {
//            _connStr = config.GetConnectionString("DefaultConnection")!;
//        }
//        //Get api/BanSao
//        [HttpGet]
//        public async Task<IActionResult> GetAll()
//        {
//            var list = new List<.BanSaoModel>();
//            using var con = new SqlConnection(_connStr);
//            await con.OpenAsync();
//            using var cmd = new SqlCommand("sp_GetAllBanDoc", con);
//            cmd.CommandType = CommandType.StoredProcedure;

//            using var rd = await cmd.ExecuteReaderAsync();
//            while (await rd.ReadAsync())
//            {
//                list.Add(new Models.BanSaoModel
//                {
//                    maBanSao = rd.GetString(0),
//                    maVach = rd.GetString(1),
//                    maSach = rd.GetString(2),
//                    maKe = rd.GetString(3),
//                    trangThai = rd.GetString(4)
//                };
//                return Ok(sach);
//            }
//            return NotFound(new { message = "Không tìm thấy bản sao" });
//        }

//        // POST api/BanSao
//        [HttpPost]
//        public async Task<IActionResult> Create(Models.BanSaoModel input)
//        {
//            var newId = input.maBanSao ?? "BS" + Guid.NewGuid().ToString("N")[..7].ToUpper();
//            using var con = new SqlConnection(_connStr);
//            await con.OpenAsync();
//            using var cmd = new SqlCommand("sp_InsertBanSao", con);
//            cmd.CommandType = CommandType.StoredProcedure;

//            cmd.Parameters.AddWithValue("@MaBanSao", newId);
//            cmd.Parameters.AddWithValue("@MaVach", input.maVach);
//            cmd.Parameters.AddWithValue("@MaSach", input.maSach);
//            cmd.Parameters.AddWithValue("@MaKe", (object?)input.maKe ?? DBNull.Value);
//            cmd.Parameters.AddWithValue("@TrangThai", (object?)input.trangThai ?? DBNull.Value);

//            try
//            {
//                var rows = await cmd.ExecuteNonQueryAsync();
//                if (rows > 0)
//                {
//                    input.maBanSao = newId;
//                    return Ok(new { message = "Thêm thành công", data = input });
//                }
//                return StatusCode(500, new { message = "Không thêm được" });
//            }
//            catch (SqlException ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//        }
//        // PUT api/BanSao/{id}
//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(string id, [FromBody] Models.BanSaoModel input)
//        {
//            using var con = new SqlConnection(_connStr);
//            await con.OpenAsync();
//            using var cmd = new SqlCommand("sp_UpdateBanSao", con);
//            cmd.CommandType = CommandType.StoredProcedure;

//            cmd.Parameters.AddWithValue("@MaBanSao", id);
//            cmd.Parameters.AddWithValue("@MaVach", input.maVach);
//            cmd.Parameters.AddWithValue("@MaSach", input.maSach);
//            cmd.Parameters.AddWithValue("@MaKe", (object?)input.maKe ?? DBNull.Value);
//            cmd.Parameters.AddWithValue("@TrangThai", (object?)input.trangThai ?? DBNull.Value);

//            var rows = await cmd.ExecuteNonQueryAsync();
//            if (rows > 0) return Ok(new { message = "Cập nhật thành công" });
//            return NotFound(new { message = "Không tìm thấy bản sao" });
//        }
//        // DELETE api/BanSao/{id}
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(string id)
//        {
//            using var con = new SqlConnection(_connStr);
//            await con.OpenAsync();
//            using var cmd = new SqlCommand("sp_DeleteBanSao", con);
//            cmd.CommandType = CommandType.StoredProcedure;
//            cmd.Parameters.AddWithValue("@MaBanSao", id);

//            var rows = await cmd.ExecuteNonQueryAsync();
//            if (rows > 0) return Ok(new { message = "Xoá thành công" });
//            return NotFound(new { message = "Không tìm thấy bản sao" });
//        }
//                });
//            }
//            return Ok(list);
//        }
//        // GET api/BanSao/{id}
//        [HttpGet("{id}")]
//        public async Task<IActionResult> GetById(string id)
//        {
//            using var con = new SqlConnection(_connStr);
//            await con.OpenAsync();
//            using var cmd = new SqlCommand("sp_GetBanSaoById", con);
//            cmd.CommandType = CommandType.StoredProcedure;
//            cmd.Parameters.AddWithValue("@MaBanSao", id);

//            using var rd = await cmd.ExecuteReaderAsync();
//            if (await rd.ReadAsync())
//            {
//                var sach = new Models.BanSaoModel
//                {
//                    maBanSao = rd.GetString(0),
//                    maVach = rd.GetString(1),
//                    maSach = rd.GetString(2),
//                    maKe = rd.GetString(3),
//                    trangThai = rd.GetString(4)
//                };
//                return Ok(sach);
//            }
//            return NotFound(new { message = "Không tìm thấy bản sao" });
//        }

//        // POST api/BanSao
//        [HttpPost]
//        public async Task<IActionResult> Create(Models.BanSaoModel input)
//        {
//            var newId = input.maBanSao ?? "BS" + Guid.NewGuid().ToString("N")[..7].ToUpper();
//            using var con = new SqlConnection(_connStr);
//            await con.OpenAsync();
//            using var cmd = new SqlCommand("sp_InsertBanSao", con);
//            cmd.CommandType = CommandType.StoredProcedure;

//            cmd.Parameters.AddWithValue("@MaBanSao", newId);
//            cmd.Parameters.AddWithValue("@MaVach", input.maVach);
//            cmd.Parameters.AddWithValue("@MaSach", input.maSach);
//            cmd.Parameters.AddWithValue("@MaKe", (object?)input.maKe ?? DBNull.Value);
//            cmd.Parameters.AddWithValue("@TrangThai", (object?)input.trangThai ?? DBNull.Value);

//            try
//            {
//                var rows = await cmd.ExecuteNonQueryAsync();
//                if (rows > 0)
//                {
//                    input.maBanSao = newId;
//                    return Ok(new { message = "Thêm thành công", data = input });
//                }
//                return StatusCode(500, new { message = "Không thêm được" });
//            }
//            catch (SqlException ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//        }
//        // PUT api/BanSao/{id}
//        [HttpPut("{id}")]
//        public async Task<IActionResult> Update(string id, [FromBody] Models.BanSaoModel input)
//        {
//            using var con = new SqlConnection(_connStr);
//            await con.OpenAsync();
//            using var cmd = new SqlCommand("sp_UpdateBanSao", con);
//            cmd.CommandType = CommandType.StoredProcedure;

//            cmd.Parameters.AddWithValue("@MaBanSao", id);
//            cmd.Parameters.AddWithValue("@MaVach", input.maVach);
//            cmd.Parameters.AddWithValue("@MaSach", input.maSach);
//            cmd.Parameters.AddWithValue("@MaKe", (object?)input.maKe ?? DBNull.Value);
//            cmd.Parameters.AddWithValue("@TrangThai", (object?)input.trangThai ?? DBNull.Value);

//            var rows = await cmd.ExecuteNonQueryAsync();
//            if (rows > 0) return Ok(new { message = "Cập nhật thành công" });
//            return NotFound(new { message = "Không tìm thấy bản sao" });
//        }
//        // DELETE api/BanSao/{id}
//        [HttpDelete("{id}")]
//        public async Task<IActionResult> Delete(string id)
//        {
//            using var con = new SqlConnection(_connStr);
//            await con.OpenAsync();
//            using var cmd = new SqlCommand("sp_DeleteBanSao", con);
//            cmd.CommandType = CommandType.StoredProcedure;
//            cmd.Parameters.AddWithValue("@MaBanSao", id);

//            var rows = await cmd.ExecuteNonQueryAsync();
//            if (rows > 0) return Ok(new { message = "Xoá thành công" });
//            return NotFound(new { message = "Không tìm thấy bản sao" });
//        }

//    }
//}

=======
>>>>>>> 2e50a061e88c396942f9de0b63b0b553660fd4f0
