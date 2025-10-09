﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyWebAPI.DTO
{
    // DTO cho hiển thị thông tin sách
    public class SachDTO
    {
        public string? MaSach { get; set; }
        public string TenSach { get; set; } = default!;
        public string TacGia { get; set; } = default!;
        public int? NamXuatBan { get; set; }
        public string? TheLoai { get; set; }
        public string? NgonNgu { get; set; }
        public string? TomTat { get; set; }
    }

    // DTO cho tạo mới sách
    public class CreateSachRequest
    {
        public string? MaSach { get; set; }
        public string TenSach { get; set; } = default!;
        public string TacGia { get; set; } = default!;
        public int? NamXuatBan { get; set; }
        public string? TheLoai { get; set; }
        public string? NgonNgu { get; set; }
        public string? TomTat { get; set; }
    }

    // DTO cho cập nhật sách
    public class UpdateSachRequest
    {
        public string TenSach { get; set; } = default!;
        public string TacGia { get; set; } = default!;
        public int? NamXuatBan { get; set; }
        public string? TheLoai { get; set; }
        public string? NgonNgu { get; set; }
        public string? TomTat { get; set; }
    }

    // DTO chung cho response
    public class ResponseDTO<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = default!;
        public T? Data { get; set; }
    }
}
