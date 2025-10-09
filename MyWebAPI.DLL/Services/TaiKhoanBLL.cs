using Microsoft.Data.SqlClient;
using MyWebAPI.DAL.Repositories;
using MyWebAPI.DTO;

namespace MyWebAPI.BLL.Services
{
    // Interface - Định nghĩa các methods
    public interface ITaiKhoanService
    {
        Task<ResponseDTO<List<TaiKhoanDTO>>> GetAllAsync();
        Task<ResponseDTO<TaiKhoanDTO>> GetByIdAsync(string maTaiKhoan);
        Task<ResponseDTO<TaiKhoanDTO>> CreateAsync(CreateTaiKhoanRequest request);
        Task<ResponseDTO<bool>> UpdateAsync(string maTaiKhoan, UpdateTaiKhoanRequest request);
        Task<ResponseDTO<bool>> DeleteAsync(string maTaiKhoan);
    }

    // Implementation - Class thực thi
    public class TaiKhoanService : ITaiKhoanService
    {
        private readonly ITaiKhoanRepository _taiKhoanRepository;

        public TaiKhoanService(ITaiKhoanRepository taiKhoanRepository)
        {
            _taiKhoanRepository = taiKhoanRepository;
        }

        public async Task<ResponseDTO<List<TaiKhoanDTO>>> GetAllAsync()
        {
            try
            {
                var list = await _taiKhoanRepository.GetAllAsync();
                return new ResponseDTO<List<TaiKhoanDTO>>
                {
                    Success = true,
                    Message = "Lấy danh sách thành công",
                    Data = list
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<List<TaiKhoanDTO>>
                {
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ResponseDTO<TaiKhoanDTO>> GetByIdAsync(string maTaiKhoan)
        {
            try
            {
                var taiKhoan = await _taiKhoanRepository.GetByIdAsync(maTaiKhoan);
                if (taiKhoan == null)
                {
                    return new ResponseDTO<TaiKhoanDTO>
                    {
                        Success = false,
                        Message = "Không tìm thấy tài khoản",
                        Data = null
                    };
                }

                return new ResponseDTO<TaiKhoanDTO>
                {
                    Success = true,
                    Message = "Lấy thông tin thành công",
                    Data = taiKhoan
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<TaiKhoanDTO>
                {
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ResponseDTO<TaiKhoanDTO>> CreateAsync(CreateTaiKhoanRequest request)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(request.TenDangNhap))
                {
                    return new ResponseDTO<TaiKhoanDTO>
                    {
                        Success = false,
                        Message = "Tên đăng nhập không được để trống",
                        Data = null
                    };
                }

                if (string.IsNullOrWhiteSpace(request.MatKhau))
                {
                    return new ResponseDTO<TaiKhoanDTO>
                    {
                        Success = false,
                        Message = "Mật khẩu không được để trống",
                        Data = null
                    };
                }

                if (request.MatKhau.Length < 6)
                {
                    return new ResponseDTO<TaiKhoanDTO>
                    {
                        Success = false,
                        Message = "Mật khẩu phải có ít nhất 6 ký tự",
                        Data = null
                    };
                }

                // Generate new ID if not provided
                var newId = request.MaTaiKhoan ?? "TK" + Guid.NewGuid().ToString("N")[..7].ToUpper();

                // Hash password
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.MatKhau, 10);

                var rows = await _taiKhoanRepository.CreateAsync(request, newId, hashedPassword);

                if (rows > 0)
                {
                    var taiKhoanDTO = new TaiKhoanDTO
                    {
                        MaTaiKhoan = newId,
                        TenDangNhap = request.TenDangNhap,
                        MatKhau = hashedPassword,
                        VaiTro = request.VaiTro
                    };

                    return new ResponseDTO<TaiKhoanDTO>
                    {
                        Success = true,
                        Message = "Thêm tài khoản thành công",
                        Data = taiKhoanDTO
                    };
                }

                return new ResponseDTO<TaiKhoanDTO>
                {
                    Success = false,
                    Message = "Không thêm được tài khoản",
                    Data = null
                };
            }
            catch (SqlException ex)
            {
                return new ResponseDTO<TaiKhoanDTO>
                {
                    Success = false,
                    Message = $"Lỗi database: {ex.Message}",
                    Data = null
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<TaiKhoanDTO>
                {
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                    Data = null
                };
            }
        }

        public async Task<ResponseDTO<bool>> UpdateAsync(string maTaiKhoan, UpdateTaiKhoanRequest request)
        {
            try
            {
                // Validate input
                if (string.IsNullOrWhiteSpace(request.TenDangNhap))
                {
                    return new ResponseDTO<bool>
                    {
                        Success = false,
                        Message = "Tên đăng nhập không được để trống",
                        Data = false
                    };
                }

                if (string.IsNullOrWhiteSpace(request.MatKhau))
                {
                    return new ResponseDTO<bool>
                    {
                        Success = false,
                        Message = "Mật khẩu không được để trống",
                        Data = false
                    };
                }

                if (request.MatKhau.Length < 6)
                {
                    return new ResponseDTO<bool>
                    {
                        Success = false,
                        Message = "Mật khẩu phải có ít nhất 6 ký tự",
                        Data = false
                    };
                }

                // Hash password
                var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.MatKhau, 10);

                var rows = await _taiKhoanRepository.UpdateAsync(maTaiKhoan, request, hashedPassword);

                if (rows > 0)
                {
                    return new ResponseDTO<bool>
                    {
                        Success = true,
                        Message = "Cập nhật thành công",
                        Data = true
                    };
                }

                return new ResponseDTO<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy tài khoản",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>
                {
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                    Data = false
                };
            }
        }

        public async Task<ResponseDTO<bool>> DeleteAsync(string maTaiKhoan)
        {
            try
            {
                var rows = await _taiKhoanRepository.DeleteAsync(maTaiKhoan);

                if (rows > 0)
                {
                    return new ResponseDTO<bool>
                    {
                        Success = true,
                        Message = "Xoá thành công",
                        Data = true
                    };
                }

                return new ResponseDTO<bool>
                {
                    Success = false,
                    Message = "Không tìm thấy tài khoản",
                    Data = false
                };
            }
            catch (Exception ex)
            {
                return new ResponseDTO<bool>
                {
                    Success = false,
                    Message = $"Lỗi: {ex.Message}",
                    Data = false
                };
            }
        }
    }
}