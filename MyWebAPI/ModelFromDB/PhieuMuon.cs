using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.ModelFromDB;

[Table("PhieuMuon")]
public partial class PhieuMuon
{
    [Key]
    [StringLength(20)]
    public string MaPhieuMuon { get; set; } = null!;

    [StringLength(20)]
    public string MaBanSao { get; set; } = null!;

    [StringLength(20)]
    public string MaBanDoc { get; set; } = null!;

    public DateOnly NgayMuon { get; set; }

    public DateOnly HanTra { get; set; }

    public DateOnly NgayTra { get; set; }

    public int SoLanGiaHan { get; set; }

    [StringLength(10)]
    public string? TrangThai { get; set; }

    [ForeignKey("MaBanDoc")]
    [InverseProperty("PhieuMuons")]
    public virtual BanDoc MaBanDocNavigation { get; set; } = null!;

    [ForeignKey("MaBanSao")]
    [InverseProperty("PhieuMuons")]
    public virtual BanSao MaBanSaoNavigation { get; set; } = null!;

    [InverseProperty("MaPhieuMuonNavigation")]
    public virtual ICollection<Phat> Phats { get; set; } = new List<Phat>();
}
