using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.ModelFromDB;

[Table("Phat")]
public partial class Phat
{
    [Key]
    [StringLength(20)]
    public string MaPhat { get; set; } = null!;

    [StringLength(20)]
    public string MaPhieuMuon { get; set; } = null!;

    [Column(TypeName = "decimal(12, 2)")]
    public decimal SoTien { get; set; }

    [StringLength(20)]
    public string LyDo { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime NgayTinh { get; set; }

    [StringLength(20)]
    public string TrangThai { get; set; } = null!;

    [StringLength(20)]
    public string? MaThanhToan { get; set; }

    [ForeignKey("MaPhieuMuon")]
    [InverseProperty("Phats")]
    public virtual PhieuMuon MaPhieuMuonNavigation { get; set; } = null!;

    [ForeignKey("MaThanhToan")]
    [InverseProperty("Phats")]
    public virtual ThanhToan? MaThanhToanNavigation { get; set; }
}
