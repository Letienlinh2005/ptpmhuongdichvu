using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.ModelFromDB;

[Table("TheLoai")]
[Index("TenTheLoai", Name = "UQ__TheLoai__327F958F9393C232", IsUnique = true)]
public partial class TheLoai
{
    [Key]
    [StringLength(20)]
    public string MaTheLoai { get; set; } = null!;

    [StringLength(100)]
    public string TenTheLoai { get; set; } = null!;

    [StringLength(255)]
    public string? MoTa { get; set; }

    [InverseProperty("MaTheLoaiNavigation")]
    public virtual ICollection<Sach> Saches { get; set; } = new List<Sach>();
}
