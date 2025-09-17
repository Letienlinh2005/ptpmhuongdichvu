using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.ModelFromDB;

[Table("DatCho")]
public partial class DatCho
{
    [Key]
    [StringLength(20)]
    public string MaDatCho { get; set; } = null!;

    [StringLength(20)]
    public string MaSach { get; set; } = null!;

    [StringLength(20)]
    public string MaBanDoc { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime NgayTao { get; set; }

    [StringLength(20)]
    public string TrangThai { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? GiuDen { get; set; }

    [ForeignKey("MaBanDoc")]
    [InverseProperty("DatChos")]
    public virtual BanDoc MaBanDocNavigation { get; set; } = null!;

    [ForeignKey("MaSach")]
    [InverseProperty("DatChos")]
    public virtual Sach MaSachNavigation { get; set; } = null!;
}
