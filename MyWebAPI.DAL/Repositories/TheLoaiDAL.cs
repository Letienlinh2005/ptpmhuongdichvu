using Microsoft.Data.SqlClient;
using System.Data;
using MyWebAPI.DTO;

namespace MyWebAPI.DAL.Repositories
{
    public interface ITheLoaiDAL
    {
        Task<List<TheLoaiDTO>> GetAllTheLoaiAsync();
        Task<TheLoaiDTO?> GetTheLoaiByIdAsync(string maTheLoai);
        Task<bool> CreateTheLoaiAsync(TheLoaiDTO theLoai);
        Task<bool> UpdateTheLoaiAsync(string maTheLoai, TheLoaiDTO theLoai);
        Task<bool> DeleteTheLoaiAsync(string maTheLoai);
    }
    public class TheLoaiDAL : ITheLoaiDAL
    {
        private readonly string _connStr;
        
        public async Task<List<TheLoaiDTO>> GetAllTheLoaiAsync()
        {
            var list = new List<TheLoaiDTO>();
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetAllTheLoai", con);
            cmd.CommandType = CommandType.StoredProcedure;
            using var rd = await cmd.ExecuteReaderAsync();
            while (await rd.ReadAsync())
            {
                list.Add(new TheLoaiDTO
                {
                    MaTheLoai = rd.GetString(0),
                    TenTheLoai = rd.GetString(1)
                });
            }
            return list;
        }
        public async Task<TheLoaiDTO?> GetTheLoaiByIdAsync(string maTheLoai)
        {
            TheLoaiDTO? theLoai = null;
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_GetTheLoaiById", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaTheLoai", maTheLoai);
            using var rd = await cmd.ExecuteReaderAsync();
            if (await rd.ReadAsync())
            {
                theLoai = new TheLoaiDTO
                {
                    MaTheLoai = rd.GetString(0),
                    TenTheLoai = rd.GetString(1)
                };
            }
            return theLoai;
        }
        public async Task<bool> CreateTheLoaiAsync(TheLoaiDTO theLoai)
        {
            using var con = new SqlConnection(_connStr);
            await con.OpenAsync();
            using var cmd = new SqlCommand("sp_CreateTheLoai", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MaTheLoai", theLoai.MaTheLoai);
            cmd.Parameters.AddWithValue("@TenTheLoai", theLoai.TenTheLoai);
            var rowsAffected = await cmd.ExecuteNonQueryAsync();
            return rowsAffected > 0;
        }
        public async Task<bool> UpdateTheLoaiAsync(string maTheLoai, TheLoaiDTO theLoai)
        {
            using var con = new SqlConnection(_connStr);
            await
}
