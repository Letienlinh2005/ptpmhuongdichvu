namespace MyWebAPI.DTO
{
    public class TaiKhoanDTO
    {
        public string? MaTaiKhoan { get; set; }
        public string TenDangNhap { get; set; } = default!;
        public string MatKhau { get; set; } = default!;
        public string VaiTro { get; set; } = default!;
    }

    public class CreateTaiKhoanRequest
    {
        public string? MaTaiKhoan { get; set; }
        public string TenDangNhap { get; set; } = default!;
        public string MatKhau { get; set; } = default!;
        public string VaiTro { get; set; } = default!;
    }

    public class UpdateTaiKhoanRequest
    {
        public string TenDangNhap { get; set; } = default!;
        public string MatKhau { get; set; } = default!;
        public string VaiTro { get; set; } = default!;
    }

}