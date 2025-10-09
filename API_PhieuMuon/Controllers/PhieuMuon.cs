<<<<<<< HEAD
﻿using API_PhieuMuon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_PhieuMuon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuMuon : ControllerBase
    {
        private readonly string _connStr;
        public PhieuMuon(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection")!;
        }

        // GET api/phieumuon
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = new List<Models.PhieuMuonModels>();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetAllPhieuMuon", con);
            cmd.CommandType = CommandType.StoredProcedure;
            using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new Models.PhieuMuonModels
                {
                    maPhieuMuon = rd.GetString(0),
                    maBanSao= rd.GetString(1),
                    maBanDoc= rd.GetString(2),
                    ngayMuon = DateOnly.FromDateTime(rd.GetDateTime(3)),
                    hanTra = DateOnly.FromDateTime(rd.GetDateTime(4)),
                    ngayTra = DateOnly.FromDateTime(rd.GetDateTime(5)),
                    soLanGiaHan = rd.GetInt64(6),
                    trangThai = rd.GetString(7)
                });
            }
            return Ok(list);
        }
        // GET api/bandoc/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Models.PhieuMuonModels? phieumuon = null;
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetPhieuMuonById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaPhieuMuon", id);
            using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                phieumuon = new Models.PhieuMuonModels
                {
                    maPhieuMuon = rd.GetString(0),
                    maBanSao = rd.GetString(1),
                    maBanDoc = rd.GetString(2),
                    ngayMuon = DateOnly.FromDateTime(rd.GetDateTime(3)),
                    hanTra = DateOnly.FromDateTime(rd.GetDateTime(4)),
                    ngayTra = DateOnly.FromDateTime(rd.GetDateTime(5)),
                    soLanGiaHan = rd.GetInt64(6),
                    trangThai = rd.GetString(7)
                };
            }
            if (phieumuon == null)
                return NotFound(new { message = "Không tìm thấy phiếu mượn" });
            return Ok(phieumuon);
        }
        // POST api/PhieuMuon
        [HttpPost]
        public async Task<IActionResult> Create(Models.PhieuMuonModels phieumuon)
        {
            var newId = phieumuon.maPhieuMuon ?? "BD" + Guid.NewGuid().ToString("N")[..7].ToUpper();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_CreatePhieuMuon", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaPhieuMuon", newId);
            cmd.Parameters.AddWithValue("@MaPhieuMuon", phieumuon.maPhieuMuon);
            cmd.Parameters.AddWithValue("@MaBanSao", phieumuon.maBanSao);
            cmd.Parameters.AddWithValue("@MaBanDoc", phieumuon.maBanDoc);
            cmd.Parameters.AddWithValue("@NgayMuon", phieumuon.ngayMuon.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@HanTra", phieumuon.hanTra.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@NgayTra", phieumuon.ngayTra.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@SoLanGiaHan", phieumuon.soLanGiaHan);
            cmd.Parameters.AddWithValue("@TrangThai", phieumuon.trangThai);
            try
            {
                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
                    phieumuon.maBanDoc = newId;
                    return Ok(new { message = "Thêm thành công", data = phieumuon });
                }
                return StatusCode(500, new { message = "Không thêm được" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        // PUT api/PhieuMuon/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Models.PhieuMuonModels phieumuon)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_UpdatePhieuMuon", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaPhieuMuon", id);
            cmd.Parameters.AddWithValue("@MaBanSao", phieumuon.maBanSao);
            cmd.Parameters.AddWithValue("@MaBanDoc", phieumuon.maBanDoc);
            cmd.Parameters.AddWithValue("@NgayMuon", phieumuon.ngayMuon.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@HanTra", phieumuon.hanTra.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@NgayTra", phieumuon.ngayTra.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@SoLanGiaHan", phieumuon.soLanGiaHan);
            cmd.Parameters.AddWithValue("@TrangThai", phieumuon.trangThai);
            try
            {
                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
                    return Ok(new { message = "Cập nhật thành công", data = phieumuon });
                }
                return NotFound(new { message = "Không tìm thấy phiếu mượn" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
=======
﻿using API_PhieuMuon.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace API_PhieuMuon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhieuMuon : ControllerBase
    {
        private readonly string _connStr;
        public PhieuMuon(IConfiguration config)
        {
            _connStr = config.GetConnectionString("DefaultConnection")!;
        }

        // GET api/phieumuon
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var list = new List<Models.PhieuMuonModels>();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetAllPhieuMuon", con);
            cmd.CommandType = CommandType.StoredProcedure;
            using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new Models.PhieuMuonModels
                {
                    maPhieuMuon = rd.GetString(0),
                    maBanSao= rd.GetString(1),
                    maBanDoc= rd.GetString(2),
                    ngayMuon = DateOnly.FromDateTime(rd.GetDateTime(3)),
                    hanTra = DateOnly.FromDateTime(rd.GetDateTime(4)),
                    ngayTra = DateOnly.FromDateTime(rd.GetDateTime(5)),
                    soLanGiaHan = rd.GetInt64(6),
                    trangThai = rd.GetString(7)
                });
            }
            return Ok(list);
        }
        // GET api/bandoc/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            Models.PhieuMuonModels? phieumuon = null;
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetPhieuMuonById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaPhieuMuon", id);
            using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                phieumuon = new Models.PhieuMuonModels
                {
                    maPhieuMuon = rd.GetString(0),
                    maBanSao = rd.GetString(1),
                    maBanDoc = rd.GetString(2),
                    ngayMuon = DateOnly.FromDateTime(rd.GetDateTime(3)),
                    hanTra = DateOnly.FromDateTime(rd.GetDateTime(4)),
                    ngayTra = DateOnly.FromDateTime(rd.GetDateTime(5)),
                    soLanGiaHan = rd.GetInt64(6),
                    trangThai = rd.GetString(7)
                };
            }
            if (phieumuon == null)
                return NotFound(new { message = "Không tìm thấy phiếu mượn" });
            return Ok(phieumuon);
        }
        // POST api/PhieuMuon
        [HttpPost]
        public async Task<IActionResult> Create(Models.PhieuMuonModels phieumuon)
        {
            var newId = phieumuon.maPhieuMuon ?? "BD" + Guid.NewGuid().ToString("N")[..7].ToUpper();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_CreatePhieuMuon", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaPhieuMuon", newId);
            cmd.Parameters.AddWithValue("@MaPhieuMuon", phieumuon.maPhieuMuon);
            cmd.Parameters.AddWithValue("@MaBanSao", phieumuon.maBanSao);
            cmd.Parameters.AddWithValue("@MaBanDoc", phieumuon.maBanDoc);
            cmd.Parameters.AddWithValue("@NgayMuon", phieumuon.ngayMuon.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@HanTra", phieumuon.hanTra.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@NgayTra", phieumuon.ngayTra.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@SoLanGiaHan", phieumuon.soLanGiaHan);
            cmd.Parameters.AddWithValue("@TrangThai", phieumuon.trangThai);
            try
            {
                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
                    phieumuon.maBanDoc = newId;
                    return Ok(new { message = "Thêm thành công", data = phieumuon });
                }
                return StatusCode(500, new { message = "Không thêm được" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { message = ex.Message });
            }

        }
        // PUT api/PhieuMuon/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, Models.PhieuMuonModels phieumuon)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_UpdatePhieuMuon", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaPhieuMuon", id);
            cmd.Parameters.AddWithValue("@MaBanSao", phieumuon.maBanSao);
            cmd.Parameters.AddWithValue("@MaBanDoc", phieumuon.maBanDoc);
            cmd.Parameters.AddWithValue("@NgayMuon", phieumuon.ngayMuon.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@HanTra", phieumuon.hanTra.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@NgayTra", phieumuon.ngayTra.ToDateTime(new TimeOnly(0, 0)));
            cmd.Parameters.AddWithValue("@SoLanGiaHan", phieumuon.soLanGiaHan);
            cmd.Parameters.AddWithValue("@TrangThai", phieumuon.trangThai);
            try
            {
                var rows = await cmd.ExecuteNonQueryAsync();
                if (rows > 0)
                {
                    return Ok(new { message = "Cập nhật thành công", data = phieumuon });
                }
                return NotFound(new { message = "Không tìm thấy phiếu mượn" });
            }
            catch (SqlException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
>>>>>>> 69b5c1abb6364dc1f32b69a88f04c05a5d1e4b2d
