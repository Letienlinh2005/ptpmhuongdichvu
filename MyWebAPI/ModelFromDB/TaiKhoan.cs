using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.ModelFromDB;

[Table("TaiKhoan")]
[Index("TenDangNhap", Name = "UQ__TaiKhoan__55F68FC0EC635BE8", IsUnique = true)]
public partial class TaiKhoan
{
    [Key]
    [StringLength(20)]
    public string MaTaiKhoan { get; set; } = null!;

    [StringLength(100)]
    public string TenDangNhap { get; set; } = null!;

    [StringLength(255)]
    public string MatKhau { get; set; } = null!;

    [StringLength(20)]
    public string VaiTro { get; set; } = null!;

    [StringLength(20)]
    public string? MaBanDoc { get; set; }

    [ForeignKey("MaBanDoc")]
    [InverseProperty("TaiKhoans")]
    public virtual BanDoc? MaBanDocNavigation { get; set; }
}
