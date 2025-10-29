using System;
<<<<<<< HEAD
=======
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
>>>>>>> 03c0194f215dea2f4a6e12e7c2473f2d8a6a88d2

namespace MyWebAPI.DTO
{
    public class PhatDTO
    {
<<<<<<< HEAD
        public string MaPhat { get; set; }
        public string MaPhieuMuon { get; set; }
        public decimal SoTien { get; set; }
        public string LyDo { get; set; }

        public DateTime NgayTinh { get; set; }
        public string TrangThai { get; set; }
        public string? MaThanhToan { get; set; }
    }

    // Request khi trả sách
    public class TraSachDTO
    {
        public string MaPhieuMuon { get; set; } = string.Empty;
        public DateTime NgayTraThucTe { get; set; }
    }

    // Response tính phạt
    public class TraSachResultDTO
    {
        public string MaPhieuMuon { get; set; } = string.Empty;
        public int SoNgayTre { get; set; }
        public decimal TienPhat { get; set; }
        public string? MaPhat { get; set; }
=======
>>>>>>> 03c0194f215dea2f4a6e12e7c2473f2d8a6a88d2
    }
}
