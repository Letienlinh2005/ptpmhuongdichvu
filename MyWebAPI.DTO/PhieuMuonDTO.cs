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
<<<<<<< HEAD

=======
<<<<<<< HEAD
        public DateTime? NgayTraThucTe { get; set; }
        public int SoLanGiaHan { get; set; }
        public string TrangThai { get; set; }
=======

>>>>>>> e4f0c2642b00fdb8eaf11ca7e3d59ede6e6b60e4
>>>>>>> fa682c200526312ec03954f6141e3bf1a74a6f44
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