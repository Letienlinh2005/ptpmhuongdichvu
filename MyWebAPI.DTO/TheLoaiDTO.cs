

namespace MyWebAPI.DTO
{
    public class TheLoaiDTO
    {
        public string MaTheLoai { get; set; } = null!;
        public string TenTheLoai { get; set; } = null!;
        public string? MoTa { get; set; }
    }

    public class CreateTheLoaiDTO
    {
        public string? MaTheLoai { get; set; }
        public string TenTheLoai { get; set; } = null!;
        public string? MoTa { get; set; }

    }
    public class UpdateTheLoaiDTO
    {
        public string TenTheLoai { get; set; } = null!;
        public string? MoTa { get; set; }
    }
}
