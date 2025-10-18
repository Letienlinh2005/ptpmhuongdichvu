using MyWebAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebAPI.DAL.Repositories
{
    public interface IThanhToanRepository
    {
        Task<ResponseDTO<List<PhieuMuonDTO>>> GetAllAsync();
        Task<ResponseDTO<PhieuMuonDTO>> GetByIdAsync(string maThanhToan);
        Task<ResponseDTO<ThanhToanDTO>> PayAsync(ThanhToanCreateDTO request);
    }
    public class ThanhToanRepository : IThanhToanRepository
    {
        private readonly string _connStr;
        public ThanhToanRepository(string connectionString)
        {
            _connStr = connectionString;
        }
        public Task<ResponseDTO<List<PhieuMuonDTO>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<ResponseDTO<PhieuMuonDTO>> GetByIdAsync(string maThanhToan)
        {
            throw new NotImplementedException();
        }
        public Task<ResponseDTO<ThanhToanDTO>> PayAsync(ThanhToanCreateDTO request)
        {
            throw new NotImplementedException();
        }
    }

}
