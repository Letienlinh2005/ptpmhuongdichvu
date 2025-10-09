﻿namespace MyWebAPI.DTO
{
    // DTO cho hiển thị thông tin bạn đọc
    public class BanDocDTO
    {
        public string? MaBanDoc { get; set; }
        public string HoTen { get; set; } = default!;
        public string SoThe { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string DienThoai { get; set; } = default!;
        public DateOnly HanThe { get; set; }
        public string TrangThaiThe { get; set; } = default!;
        public decimal DuNo { get; set; }
    }

    // DTO cho tạo mới bạn đọc
    public class CreateBanDocRequest
    {
        public string? MaBanDoc { get; set; }
        public string HoTen { get; set; } = default!;
        public string SoThe { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string DienThoai { get; set; } = default!;
        public DateOnly HanThe { get; set; }
        public string TrangThaiThe { get; set; } = default!;
        public decimal DuNo { get; set; }
    }

    // DTO cho cập nhật bạn đọc
    public class UpdateBanDocRequest
    {
        public string HoTen { get; set; } = default!;
        public string SoThe { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string DienThoai { get; set; } = default!;
        public DateOnly HanThe { get; set; }
        public string TrangThaiThe { get; set; } = default!;
        public decimal DuNo { get; set; }
    }
}