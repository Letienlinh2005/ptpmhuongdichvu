
﻿using MyWebAPI.BLL;
using MyWebAPI.BLL.Services;
using MyWebAPI.DAL;
using MyWebAPI.DAL.Repositories;
using static MyWebAPI.BLL.Services.PhatBLL;
using static MyWebAPI.DAL.Repositories.PhatDAL;
using static MyWebAPI.DAL.Repositories.PhieuMuonDAL;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


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


builder.Services.AddScoped<IPhatRepository>(_ => new PhatRepository(connectionString));
builder.Services.AddScoped<IPhatService, PhatService>();

builder.Services.AddScoped<IThanhToanRepository>(_ => new ThanhToanRepository(connectionString));
builder.Services.AddScoped<IThanhToanService, ThanhToanService>();


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();


app.Run();




