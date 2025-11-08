<<<<<<< HEAD
using MyWebAPI.BLL;
=======

﻿using MyWebAPI.BLL;
>>>>>>> 2e50a061e88c396942f9de0b63b0b553660fd4f0
using MyWebAPI.BLL.Services;
using MyWebAPI.DAL;
using MyWebAPI.DAL.Repositories;
using static MyWebAPI.BLL.Services.PhatBLL;
using static MyWebAPI.DAL.Repositories.PhatDAL;
using static MyWebAPI.DAL.Repositories.PhieuMuonDAL;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ===== CORS =====
const string CorsPolicy = "DevCors";
builder.Services.AddCors(o =>
{
    o.AddPolicy(CorsPolicy, p =>
    {
        p.WithOrigins(
            "http://127.0.0.1:5500", // Live Server
            "http://localhost:5500", // nếu dùng localhost
            "https://localhost:7053" // nếu sau này chạy qua gateway/FE https
        )
        .AllowAnyHeader()
        .AllowAnyMethod();
        // .AllowCredentials(); // chỉ bật nếu dùng cookie từ trình duyệt
    });
});
// =================

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ISachRepository>(_ => new SachRepository(connectionString));
builder.Services.AddScoped<ISachService, SachService>();

builder.Services.AddScoped<IBanSaoRepository>(_ => new BanSaoRepository(connectionString));
builder.Services.AddScoped<IBanSaoService, BanSaoService>();

builder.Services.AddScoped<ITaiKhoanRepository>(_ => new TaiKhoanRepository(connectionString));
builder.Services.AddScoped<ITaiKhoanService, TaiKhoanService>();

builder.Services.AddScoped<ITheLoaiRepository>(_ => new TheLoaiRepository(connectionString));
builder.Services.AddScoped<ITheLoaiService, TheLoaiService>();

builder.Services.AddScoped<IKeSachRepository>(_ => new KeSachRepository(connectionString));
builder.Services.AddScoped<IKeSachService, KeSachService>();

builder.Services.AddScoped<IPhieuMuonRepository>(_ => new PhieuMuonRepository(connectionString));
builder.Services.AddScoped<IPhieuMuonService, PhieuMuonService>();

builder.Services.AddScoped<IBanDocRepository>(_ => new BanDocRepository(connectionString));
builder.Services.AddScoped<IBanDocService, BanDocService>();

builder.Services.AddScoped<IDatChoStorage>(_ => new SqlDatChoStorage(connectionString));
builder.Services.AddScoped<IDatChoService, DatChoService>();

<<<<<<< HEAD
=======

>>>>>>> 2e50a061e88c396942f9de0b63b0b553660fd4f0
builder.Services.AddScoped<IPhatRepository>(_ => new PhatRepository(connectionString));
builder.Services.AddScoped<IPhatService, PhatService>();

builder.Services.AddScoped<IThanhToanRepository>(_ => new ThanhToanRepository(connectionString));
builder.Services.AddScoped<IThanhToanService, ThanhToanService>();

<<<<<<< HEAD
builder.Services.AddScoped<ISachRepository>(_ => new SachRepository(connectionString));
builder.Services.AddScoped<ISachService, SachService> ();
=======
>>>>>>> 2e50a061e88c396942f9de0b63b0b553660fd4f0

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
//app.UseHttpsRedirection();

// ===== BẬT CORS TRƯỚC UseAuthorization/MapControllers =====
app.UseCors(CorsPolicy);
// ==========================================================

app.UseAuthorization();

app.MapControllers();
<<<<<<< HEAD

app.Run(); // <-- Chỉ để 1 lần
=======

app.Run();


app.Run();




>>>>>>> 2e50a061e88c396942f9de0b63b0b553660fd4f0
