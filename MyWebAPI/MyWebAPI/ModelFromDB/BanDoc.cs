using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.ModelFromDB;

[Table("BanDoc")]
[Index("SoThe", Name = "UQ__BanDoc__26CB8FE8EDF5F148", IsUnique = true)]
public partial class BanDoc
{
    [Key]
    [StringLength(20)]
    public string MaBanDoc { get; set; } = null!;

    [StringLength(10)]
    public string SoThe { get; set; } = null!;

    [StringLength(100)]
    public string HoTen { get; set; } = null!;

    [StringLength(100)]
    public string? Email { get; set; }

    [StringLength(10)]
    public string? DienThoai { get; set; }

    public DateOnly HanThe { get; set; }

    [StringLength(20)]
    public string TrangThaiThe { get; set; } = null!;

    [Column(TypeName = "decimal(12, 2)")]
    public decimal DuNo { get; set; }

    [InverseProperty("MaBanDocNavigation")]
    public virtual ICollection<DatCho> DatChos { get; set; } = new List<DatCho>();

    [InverseProperty("MaBanDocNavigation")]
    public virtual ICollection<PhieuMuon> PhieuMuons { get; set; } = new List<PhieuMuon>();

    [InverseProperty("MaBanDocNavigation")]
    public virtual ICollection<TaiKhoan> TaiKhoans { get; set; } = new List<TaiKhoan>();

    [InverseProperty("MaBanDocNavigation")]
    public virtual ICollection<ThanhToan> ThanhToans { get; set; } = new List<ThanhToan>();
}
