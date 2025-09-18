using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.ModelFromDB;

[Table("Sach")]
public partial class Sach
{
    [Key]
    [StringLength(20)]
    public string MaSach { get; set; } = null!;

    [StringLength(255)]
    public string TieuDe { get; set; } = null!;

    [StringLength(255)]
    public string TacGia { get; set; } = null!;

    [StringLength(20)]
    public string? MaTheLoai { get; set; }

    public int? NamXuatBan { get; set; }

    [StringLength(50)]
    public string? NgonNgu { get; set; }

    public string? TomTat { get; set; }

    [InverseProperty("MaSachNavigation")]
    public virtual ICollection<BanSao> BanSaos { get; set; } = new List<BanSao>();

    [InverseProperty("MaSachNavigation")]
    public virtual ICollection<DatCho> DatChos { get; set; } = new List<DatCho>();

    [ForeignKey("MaTheLoai")]
    [InverseProperty("Saches")]
    public virtual TheLoai? MaTheLoaiNavigation { get; set; }
}
