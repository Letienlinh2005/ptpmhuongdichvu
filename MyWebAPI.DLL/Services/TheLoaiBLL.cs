//using Microsoft.Data.SqlClient;
//using MyWebAPI.DAL.Repositories;
//using MyWebAPI.DTO;

//namespace MyWebAPI.BLL.Services
//{
//    // Interface TheLoai
//    public interface ITheLoaiService
//    {
//        Task<ResponseDTO<List<TheLoaiDTO>>> GetAllAsync();
//        Task<ResponseDTO<TheLoaiDTO>> GetByIdAsync(string id);
//        Task<ResponseDTO<TheLoaiDTO>> CreateAsync(CreateTheLoaiDTO request);
//        Task<ResponseDTO<TheLoaiDTO>> UpdateAsync(string id, UpdateTheLoaiDTO request);
//        Task<ResponseDTO<bool>> DeleteAsync(string id);
//    }
//    // Implementation TheLoai
//    public class TheLoaiService : ITheLoaiService
//    {
//        private readonly ITheLoaiRepository _theLoaiRepository;
//        public TheLoaiService(ITheLoaiRepository theLoaiRepository)
//        {
//            _theLoaiRepository = theLoaiRepository;
//        }
//        public async Task<ResponseDTO<List<TheLoaiDTO>>> GetAllAsync()
//        {
//            var response = new ResponseDTO<List<TheLoaiDTO>>();
//            try
//            {
//                var theLoais = await _theLoaiRepository.GetAllAsync();
//                response.Data = theLoais;
//                response.Success = true;
//            }
//            catch (SqlException ex)
//            {
//                response.Message = ex.Message;
//                response.Success = false;
//            }
//            return response;
//        }
//        public async Task<ResponseDTO<TheLoaiDTO>> GetByIdAsync(string id)
//        {
//            var response = new ResponseDTO<TheLoaiDTO>();
//            try
//            {
//                var theLoai = await _theLoaiRepository.GetByIdAsync(id);
//                if (theLoai != null)
//                {
//                    response.Data = theLoai;
//                    response.Success = true;
//                }
//                else
//                {
//                    response.Message = "TheLoai not found.";
//                    response.Success = false;
//                }
//            }
//            catch (SqlException ex)
//            {
//                response.Message = ex.Message;
//                response.Success = false;
//            }
//            return response;
//        }
//        public async Task<ResponseDTO<TheLoaiDTO>> CreateAsync(CreateTheLoaiDTO request)
//        {
//            var response = new ResponseDTO<TheLoaiDTO>();
//            try
//            {
//                var createdTheLoai = await _theLoaiRepository.CreateAsync(request);
//                response.Data = createdTheLoai;
//                response.Success = true;
//            }
//            catch (SqlException ex)
//            {
//                response.Message = ex.Message;
//                response.Success = false;
//            }
//            return response;
//        }
//        public async Task<ResponseDTO<TheLoaiDTO>> UpdateAsync(string id, UpdateTheLoaiDTO request)
//        {
//            var response = new ResponseDTO<TheLoaiDTO>();
//            try
//            {
//                var updatedTheLoai = await _theLoaiRepository.UpdateAsync(id, request);
//                if (updatedTheLoai != null)
//                {
//                    response.Data = updatedTheLoai;
//                    response.Success = true;
//                }
//                else
//                {
//                    response.Message = "TheLoai not found.";
//                    response.Success = false;
//                }
//            }
//            catch (SqlException ex)
//}
