using MyWebAPI.DAL.Repositories;
using MyWebAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebAPI.BLL.Services
{
    public interface IThanhToanService
    {
        Task<ResponseDTO<List<PhieuMuonDTO>>> GetAllAsync();
        Task<ResponseDTO<PhieuMuonDTO>> GetByIdAsync(string maThanhToan);
        Task<ResponseDTO<ThanhToanDTO>> PayAsync(ThanhToanCreateDTO request);
    }
    public class ThanhToanService : IThanhToanService
    {
        private readonly IThanhToanRepository _ThanhToanRepository;
        public Task<ResponseDTO<List<PhieuMuonDTO>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }
        public Task<ResponseDTO<PhieuMuonDTO>> GetByIdAsync(string maBanDoc)
        {
            throw new NotImplementedException();
        }
        public Task<ResponseDTO<ThanhToanDTO>> PayAsync(ThanhToanCreateDTO request)
        {
            throw new NotImplementedException();
        }
    }
}
