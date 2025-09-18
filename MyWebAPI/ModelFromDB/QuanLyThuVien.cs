using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyWebAPI.ModelFromDB;

public partial class QuanLyThuVien : DbContext
{
    public QuanLyThuVien()
    {
    }

    public QuanLyThuVien(DbContextOptions<QuanLyThuVien> options)
        : base(options)
    {
    }

    public virtual DbSet<BanDoc> BanDocs { get; set; }

    public virtual DbSet<BanSao> BanSaos { get; set; }

    public virtual DbSet<DatCho> DatChos { get; set; }

    public virtual DbSet<KeSach> KeSaches { get; set; }

    public virtual DbSet<Phat> Phats { get; set; }

    public virtual DbSet<PhieuMuon> PhieuMuons { get; set; }

    public virtual DbSet<Sach> Saches { get; set; }

    public virtual DbSet<TaiKhoan> TaiKhoans { get; set; }

    public virtual DbSet<ThanhToan> ThanhToans { get; set; }

    public virtual DbSet<TheLoai> TheLoais { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-UIS66OD;Initial Catalog=QuanLyThuVien;Integrated Security=True;Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BanDoc>(entity =>
        {
            entity.HasKey(e => e.MaBanDoc).HasName("PK__BanDoc__5D4A32779B749000");
        });

        modelBuilder.Entity<BanSao>(entity =>
        {
            entity.HasKey(e => e.MaBanSao).HasName("PK__BanSao__488BCC423BF4FFD9");

            entity.HasOne(d => d.MaKeNavigation).WithMany(p => p.BanSaos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BanSao__MaKe__46E78A0C");

            entity.HasOne(d => d.MaSachNavigation).WithMany(p => p.BanSaos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__BanSao__MaSach__45F365D3");
        });

        modelBuilder.Entity<DatCho>(entity =>
        {
            entity.HasKey(e => e.MaDatCho).HasName("PK__DatCho__707DAE6B92F51B89");

            entity.Property(e => e.NgayTao).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.MaBanDocNavigation).WithMany(p => p.DatChos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DatCho__MaBanDoc__534D60F1");

            entity.HasOne(d => d.MaSachNavigation).WithMany(p => p.DatChos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__DatCho__MaSach__52593CB8");
        });

        modelBuilder.Entity<KeSach>(entity =>
        {
            entity.HasKey(e => e.MaKe).HasName("PK__KeSach__2725CF7DEE27856D");
        });

        modelBuilder.Entity<Phat>(entity =>
        {
            entity.HasKey(e => e.MaPhat).HasName("PK__Phat__4AC072E2204FB24B");

            entity.Property(e => e.NgayTinh).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.MaPhieuMuonNavigation).WithMany(p => p.Phats)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Phat__MaPhieuMuo__5FB337D6");

            entity.HasOne(d => d.MaThanhToanNavigation).WithMany(p => p.Phats).HasConstraintName("FK__Phat__MaThanhToa__60A75C0F");
        });

        modelBuilder.Entity<PhieuMuon>(entity =>
        {
            entity.HasKey(e => e.MaPhieuMuon).HasName("PK__PhieuMuo__C4C82222D26A6D97");

            entity.HasOne(d => d.MaBanDocNavigation).WithMany(p => p.PhieuMuons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuMuon__MaBan__4D94879B");

            entity.HasOne(d => d.MaBanSaoNavigation).WithMany(p => p.PhieuMuons)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PhieuMuon__MaBan__4CA06362");
        });

        modelBuilder.Entity<Sach>(entity =>
        {
            entity.HasKey(e => e.MaSach).HasName("PK__Sach__B235742DCF6DD365");

            entity.HasOne(d => d.MaTheLoaiNavigation).WithMany(p => p.Saches).HasConstraintName("FK__Sach__MaTheLoai__3A81B327");
        });

        modelBuilder.Entity<TaiKhoan>(entity =>
        {
            entity.HasKey(e => e.MaTaiKhoan).HasName("PK__TaiKhoan__AD7C6529075CA053");

            entity.HasOne(d => d.MaBanDocNavigation).WithMany(p => p.TaiKhoans).HasConstraintName("FK__TaiKhoan__MaBanD__656C112C");
        });

        modelBuilder.Entity<ThanhToan>(entity =>
        {
            entity.HasKey(e => e.MaThanhToan).HasName("PK__ThanhToa__D4B258443AD17D6C");

            entity.Property(e => e.NgayThanhToan).HasDefaultValueSql("(sysutcdatetime())");

            entity.HasOne(d => d.MaBanDocNavigation).WithMany(p => p.ThanhToans)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ThanhToan__MaBan__59063A47");
        });

        modelBuilder.Entity<TheLoai>(entity =>
        {
            entity.HasKey(e => e.MaTheLoai).HasName("PK__TheLoai__D73FF34AAADC34AD");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
