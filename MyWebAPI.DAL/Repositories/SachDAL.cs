using Microsoft.Data.SqlClient;
using System.Data;
using MyWebAPI.DTO;

namespace MyWebAPI.DAL.Repositories
{
    public interface ISachRepository
    {
        Task<List<SachDTO>> GetAllAsync();
        Task<SachDTO?> GetByIdAsync(string maSach);
        Task<int> CreateAsync(CreateSachRequest sach, string maSach);
        Task<int> UpdateAsync(string maSach, UpdateSachRequest sach);
        Task<int> DeleteAsync(string maSach);
    }

    public class SachRepository : ISachRepository
    {
        private readonly string _connStr;

        public SachRepository(string connectionString)
        {
            _connStr = connectionString;
        }

        public async Task<List<SachDTO>> GetAllAsync()
        {
            var list = new List<SachDTO>();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetAllSach", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new SachDTO
                {
                    MaSach = rd.GetString(0),
                    TenSach = rd.GetString(1),
                    TacGia = rd.GetString(2),
                    NamXuatBan = rd.IsDBNull(3) ? null : rd.GetInt32(3),
                    TheLoai = rd.IsDBNull(4) ? null : rd.GetString(4)
                });
            }
            return list;
        }

        public async Task<SachDTO?> GetByIdAsync(string maSach)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetSachById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaSach", maSach);

            using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                return new SachDTO
                {
                    MaSach = rd.GetString(0),
                    TenSach = rd.GetString(1),
                    TacGia = rd.GetString(2),
                    NamXuatBan = rd.IsDBNull(3) ? null : rd.GetInt32(3),
                    TheLoai = rd.IsDBNull(4) ? null : rd.GetString(4)
                };
            }
            return null;
        }

        public async Task<int> CreateAsync(CreateSachRequest sach, string maSach)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_InsertSach", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MaSach", maSach);
            cmd.Parameters.AddWithValue("@TieuDe", sach.TenSach);
            cmd.Parameters.AddWithValue("@TacGia", sach.TacGia);
            cmd.Parameters.AddWithValue("@NamXuatBan", (object?)sach.NamXuatBan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaTheLoai", (object?)sach.TheLoai ?? DBNull.Value);

            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> UpdateAsync(string maSach, UpdateSachRequest sach)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_UpdateSach", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MaSach", maSach);
            cmd.Parameters.AddWithValue("@TieuDe", sach.TenSach);
            cmd.Parameters.AddWithValue("@TacGia", sach.TacGia);
            cmd.Parameters.AddWithValue("@NamXuatBan", (object?)sach.NamXuatBan ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@MaTheLoai", (object?)sach.TheLoai ?? DBNull.Value);

            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteAsync(string maSach)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_DeleteSach", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaSach", maSach);

            return await cmd.ExecuteNonQueryAsync();
        }
    }
}