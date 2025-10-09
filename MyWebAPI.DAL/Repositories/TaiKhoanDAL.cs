using Microsoft.Data.SqlClient;
using System.Data;
using MyWebAPI.DTO;

namespace MyWebAPI.DAL.Repositories
{
    // Interface - Định nghĩa các methods
    public interface ITaiKhoanRepository
    {
        Task<List<TaiKhoanDTO>> GetAllAsync();
        Task<TaiKhoanDTO?> GetByIdAsync(string maTaiKhoan);
        Task<int> CreateAsync(CreateTaiKhoanRequest taiKhoan, string maTaiKhoan, string hashedPassword);
        Task<int> UpdateAsync(string maTaiKhoan, UpdateTaiKhoanRequest taiKhoan, string hashedPassword);
        Task<int> DeleteAsync(string maTaiKhoan);
    }

    // Implementation - Class thực thi
    public class TaiKhoanRepository : ITaiKhoanRepository
    {
        private readonly string _connStr;

        public TaiKhoanRepository(string connectionString)
        {
            _connStr = connectionString;
        }

        public async Task<List<TaiKhoanDTO>> GetAllAsync()
        {
            var list = new List<TaiKhoanDTO>();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetAllTaiKhoan", con);
            cmd.CommandType = CommandType.StoredProcedure;

            using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new TaiKhoanDTO
                {
                    MaTaiKhoan = rd.GetString(0),
                    TenDangNhap = rd.GetString(1),
                    MatKhau = rd.GetString(2),
                    VaiTro = rd.GetString(3)
                });
            }
            return list;
        }

        public async Task<TaiKhoanDTO?> GetByIdAsync(string maTaiKhoan)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetTaiKhoanById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

            using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                return new TaiKhoanDTO
                {
                    MaTaiKhoan = rd.GetString(0),
                    TenDangNhap = rd.GetString(1),
                    MatKhau = rd.GetString(2),
                    VaiTro = rd.GetString(3)
                };
            }
            return null;
        }

        public async Task<int> CreateAsync(CreateTaiKhoanRequest taiKhoan, string maTaiKhoan, string hashedPassword)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_Register", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
            cmd.Parameters.AddWithValue("@TenDangNhap", taiKhoan.TenDangNhap);
            cmd.Parameters.AddWithValue("@MatKhau", hashedPassword);
            cmd.Parameters.AddWithValue("@VaiTro", taiKhoan.VaiTro);

            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> UpdateAsync(string maTaiKhoan, UpdateTaiKhoanRequest taiKhoan, string hashedPassword)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_UpdateTaiKhoan", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);
            cmd.Parameters.AddWithValue("@TenDangNhap", taiKhoan.TenDangNhap);
            cmd.Parameters.AddWithValue("@MatKhau", hashedPassword);
            cmd.Parameters.AddWithValue("@VaiTro", taiKhoan.VaiTro);

            return await cmd.ExecuteNonQueryAsync();
        }

        public async Task<int> DeleteAsync(string maTaiKhoan)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_DeleteTaiKhoan", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaTaiKhoan", maTaiKhoan);

            return await cmd.ExecuteNonQueryAsync();
        }
    }
}