using Microsoft.Data.SqlClient;
using System.Data;
using MyWebAPI.DTO;

namespace MyWebAPI.DAL.Repositories
{
    public class PhieuMuonDAL
    {
        public interface IPhieuMuonRepository
        {
            Task<List<PhieuMuonDTO>> GetAllAsync();
            Task<PhieuMuonDTO?> GetByIdAsync(string maPhieuMuon);
            Task<bool> CreateAsync(PhieuMuonDTO phieuMuon);
            Task<bool> UpdateAsync(string maPhieuMuon, PhieuMuonDTO phieuMuon);
            Task<bool> DeleteAsync(string maPhieuMuon);
        }

        public class PhieuMuonRepository : IPhieuMuonRepository
        {
            private readonly string _connStr;
            public PhieuMuonRepository(string connectionString)
            {
                _connStr = connectionString;
            }

            public async Task<List<PhieuMuonDTO>> GetAllAsync()
            {
                var list = new List<PhieuMuonDTO>();
                using var con = new SqlConnection(_connStr);
                await con.OpenAsync();
                using var cmd = new SqlCommand("sp_GetAllPhieuMuon", con);
                cmd.CommandType = CommandType.StoredProcedure;
                using var rd = await cmd.ExecuteReaderAsync();
                while (await rd.ReadAsync())
                {
                    list.Add(new PhieuMuonDTO
                    {
                        MaPhieuMuon = rd.GetString(0),
                        MaBanSao = rd.GetString(1),
                        MaBanDoc = rd.GetString(2),
                        NgayMuon = rd.GetDateTime(3),
                        HanTra = rd.GetDateTime(4),
                        NgayTraThucTe = rd.IsDBNull(5) ? (DateTime?)null : rd.GetDateTime(5),
                        SoLanGiaHan = rd.GetInt32(6),
                        TrangThai = rd.GetString(7)
                    });
                }
                return list;
            }

            public async Task<PhieuMuonDTO?> GetByIdAsync(string maPhieuMuon)
            {
                PhieuMuonDTO? phieuMuon = null;
                using var con = new SqlConnection(_connStr);
                await con.OpenAsync();
                using var cmd = new SqlCommand("sp_GetPhieuMuonById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuon);
                using var rd = await cmd.ExecuteReaderAsync();
                if (await rd.ReadAsync())
                {
                    phieuMuon = new PhieuMuonDTO
                    {
                        MaPhieuMuon = rd.GetString(0),
                        MaBanSao = rd.GetString(1),
                        MaBanDoc = rd.GetString(2),
                        NgayMuon = rd.GetDateTime(3),
                        HanTra = rd.GetDateTime(4),
                        NgayTraThucTe = rd.IsDBNull(5) ? (DateTime?)null : rd.GetDateTime(5),
                        SoLanGiaHan = rd.GetInt32(6),
                        TrangThai = rd.GetString(7)
                    };
                }
                return phieuMuon;
            }

            public async Task<bool> CreateAsync(PhieuMuonDTO phieuMuon)
            {
                using var con = new SqlConnection(_connStr);
                await con.OpenAsync();
                using var cmd = new SqlCommand("sp_TaoPhieuMuon", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaPhieuMuon", phieuMuon.MaPhieuMuon);
                cmd.Parameters.AddWithValue("@MaBanSao", phieuMuon.MaBanSao);
                cmd.Parameters.AddWithValue("@MaBanDoc", phieuMuon.MaBanDoc);
                cmd.Parameters.AddWithValue("@NgayMuon", phieuMuon.NgayMuon);
                cmd.Parameters.AddWithValue("@HanTra", phieuMuon.HanTra);
                cmd.Parameters.AddWithValue("@NgayTraThucTe", phieuMuon.NgayTraThucTe);
                cmd.Parameters.AddWithValue("@SoLanGiaHan", phieuMuon.SoLanGiaHan);
                cmd.Parameters.AddWithValue("@TrangThai", phieuMuon.TrangThai);
                cmd.Parameters.AddWithValue("@NgayTraThucTe", phieuMuon.NgayTraThucTe);
                cmd.Parameters.AddWithValue("@SoLanGiaHan", phieuMuon.SoLanGiaHan);
                cmd.Parameters.AddWithValue("@TrangThai", phieuMuon.TrangThai);
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected >= 0;
            }

            public async Task<bool> UpdateAsync(string maPhieuMuon, PhieuMuonDTO phieuMuon)
            {
                using var con = new SqlConnection(_connStr);
                await con.OpenAsync();
                using var cmd = new SqlCommand("sp_UpdatePhieuMuon", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuon);
                cmd.Parameters.AddWithValue("@MaBanSao", phieuMuon.MaBanSao);
                cmd.Parameters.AddWithValue("@MaBanDoc", phieuMuon.MaBanDoc);
                cmd.Parameters.AddWithValue("@NgayMuon", phieuMuon.NgayMuon);
                cmd.Parameters.AddWithValue("@HanTra", phieuMuon.HanTra);
                cmd.Parameters.AddWithValue("@NgayTraThucTe", (object?)phieuMuon.NgayTraThucTe ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SoLanGiaHan", phieuMuon.SoLanGiaHan);
                cmd.Parameters.AddWithValue("@TrangThai", (object?)phieuMuon.TrangThai ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@NgayTraThucTe", phieuMuon.NgayTraThucTe);
                cmd.Parameters.AddWithValue("@NgayTraThucTe",(object?)phieuMuon.NgayTraThucTe ?? DBNull.Value);
                cmd.Parameters.AddWithValue("@SoLanGiaHan", phieuMuon.SoLanGiaHan);
                cmd.Parameters.AddWithValue("@TrangThai",(object?)phieuMuon.TrangThai ?? DBNull.Value);
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }

            public async Task<bool> DeleteAsync(string maPhieuMuon)
            {
                using var con = new SqlConnection(_connStr);
                await con.OpenAsync();
                using var cmd = new SqlCommand("sp_DeletePhieuMuon", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@MaPhieuMuon", maPhieuMuon);
                var rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
}