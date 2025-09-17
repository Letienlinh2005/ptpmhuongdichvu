using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.ModelFromDB;

[Table("BanSao")]
public partial class BanSao
{
    [Key]
    [StringLength(20)]
    public string MaBanSao { get; set; } = null!;

    [StringLength(64)]
    public string MaVach { get; set; } = null!;

    [StringLength(20)]
    public string MaSach { get; set; } = null!;

    [StringLength(20)]
    public string MaKe { get; set; } = null!;

    [StringLength(20)]
    public string TrangThai { get; set; } = null!;

    [ForeignKey("MaKe")]
    [InverseProperty("BanSaos")]
    public virtual KeSach MaKeNavigation { get; set; } = null!;

    [ForeignKey("MaSach")]
    [InverseProperty("BanSaos")]
    public virtual Sach MaSachNavigation { get; set; } = null!;

    [InverseProperty("MaBanSaoNavigation")]
    public virtual ICollection<PhieuMuon> PhieuMuons { get; set; } = new List<PhieuMuon>();
}
