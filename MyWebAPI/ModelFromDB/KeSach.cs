using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.ModelFromDB;

[Table("KeSach")]
public partial class KeSach
{
    [Key]
    [StringLength(20)]
    public string MaKe { get; set; } = null!;

    [StringLength(100)]
    public string? ViTri { get; set; }

    [InverseProperty("MaKeNavigation")]
    public virtual ICollection<BanSao> BanSaos { get; set; } = new List<BanSao>();
}
