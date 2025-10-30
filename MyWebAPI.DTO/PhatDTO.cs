﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MyWebAPI.DTO
{
    public class PhatDTO
    {

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
    }
}
