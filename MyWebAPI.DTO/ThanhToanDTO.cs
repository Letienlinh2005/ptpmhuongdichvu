using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebAPI.DTO
{
    public class ThanhToanDTO
    {
        public string? MaThanhToan { get; set; }  
        public string MaBanDoc { get; set; } = default!;
        public DateTime NgayThanhToan { get; set; }  
        public decimal SoTien { get; set; }         
        public string HinhThuc { get; set; } = default!; 
        public string? GhiChu { get; set; }          
    }
    public class ThanhToanCreateDTO
    {
        public string? MaThanhToan { get; set; }     
        public string MaBanDoc { get; set; } = default!;
        public decimal SoTien { get; set; }
        public string HinhThuc { get; set; } = default!;
        public string? GhiChu { get; set; }
    }
}
