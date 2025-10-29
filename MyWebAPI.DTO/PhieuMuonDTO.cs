using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebAPI.DTO
{
    public class PhieuMuonDTO
    {
        public string MaPhieuMuon { get; set; }
        public string MaBanSao { get; set; }
        public string MaBanDoc { get; set; }
        public DateTime NgayMuon { get; set; }

        public DateTime HanTra { get; set; }
        public DateTime? NgayTraThucTe { get; set; }

        public int SoLanGiaHan { get; set; }

        public string TrangThai { get; set; }

    }

    public class CreatePhieuMuonRequest
    {
        public string MaPhieuMuon { get; set; }
        public string MaBanSao { get; set; }
        public string MaBanDoc { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime HanTra { get; set; }
        public DateTime? NgayTraThucTe { get; set; }
        public int SoLanGiaHan { get; set; }
        public string TrangThai { get; set; }
<<<<<<< HEAD
=======

>>>>>>> 03c0194f215dea2f4a6e12e7c2473f2d8a6a88d2
    }

    public class UpdatePhieuMuonRequest
    {
        public string MaBanSao { get; set; }
        public string MaBanDoc { get; set; }
        public DateTime NgayMuon { get; set; }
        public DateTime HanTra { get; set; }
        public DateTime? NgayTraThucTe { get; set; }
        public int SoLanGiaHan { get; set; }
        public string TrangThai { get; set; }
    }
}