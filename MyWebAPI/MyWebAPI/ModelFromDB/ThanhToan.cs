using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.ModelFromDB;

[Table("ThanhToan")]
public partial class ThanhToan
{
    [Key]
    [StringLength(20)]
    public string MaThanhToan { get; set; } = null!;

    [StringLength(20)]
    public string MaBanDoc { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime NgayThanhToan { get; set; }

    [Column(TypeName = "decimal(12, 2)")]
    public decimal SoTien { get; set; }

    [StringLength(20)]
    public string HinhThuc { get; set; } = null!;

    [StringLength(255)]
    public string? GhiChu { get; set; }

    [ForeignKey("MaBanDoc")]
    [InverseProperty("ThanhToans")]
    public virtual BanDoc MaBanDocNavigation { get; set; } = null!;

    [InverseProperty("MaThanhToanNavigation")]
    public virtual ICollection<Phat> Phats { get; set; } = new List<Phat>();
}
