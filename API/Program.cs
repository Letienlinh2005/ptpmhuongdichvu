<<<<<<< HEAD
﻿using MyWebAPI.BLL.Services;
=======
<<<<<<< HEAD
﻿using MyWebAPI.BLL;
using MyWebAPI.BLL.Services;
using MyWebAPI.DAL;
=======
﻿using MyWebAPI.BLL.Services;
>>>>>>> e4f0c2642b00fdb8eaf11ca7e3d59ede6e6b60e4
>>>>>>> fa682c200526312ec03954f6141e3bf1a74a6f44
using MyWebAPI.DAL.Repositories;
using static MyWebAPI.DAL.Repositories.PhieuMuonDAL;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Missing ConnectionStrings:DefaultConnection");

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

<<<<<<< HEAD
builder.Services.AddScoped<IPhieuMuonRepository>(_ => new PhieuMuonRepository(connectionString));
builder.Services.AddScoped<IPhieuMuonService, PhieuMuonService>();

builder.Services.AddScoped<IBanDocRepository>(_ => new BanDocRepository(connectionString));
builder.Services.AddScoped<IBanDocService, BanDocService>();
=======
<<<<<<< HEAD
builder.Services.AddScoped<IDatChoStorage>(_ => new SqlDatChoStorage(connectionString));
builder.Services.AddScoped<IDatChoService, DatChoService>();

// Nếu cần thêm các module khác, mở comment và đăng ký tại đây:
builder.Services.AddScoped<IPhieuMuonRepository>(_ => new PhieuMuonRepository(connectionString));
builder.Services.AddScoped<IPhieuMuonService, PhieuMuonService>();
//builder.Services.AddScoped<IBanDocRepository>(_ => new BanDocRepository(connectionString));
//builder.Services.AddScoped<IBanDocService, BanDocService>();
=======
// Nếu cần thêm các module khác, mở comment và đăng ký tại đây:
builder.Services.AddScoped<IPhieuMuonRepository>(_ => new PhieuMuonRepository(connectionString));
builder.Services.AddScoped<IPhieuMuonService, PhieuMuonService>();
builder.Services.AddScoped<IBanDocRepository>(_ => new BanDocRepository(connectionString));
builder.Services.AddScoped<IBanDocService, BanDocService>();
>>>>>>> e4f0c2642b00fdb8eaf11ca7e3d59ede6e6b60e4
>>>>>>> fa682c200526312ec03954f6141e3bf1a74a6f44

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
<<<<<<< HEAD
=======
<<<<<<< HEAD
>>>>>>> fa682c200526312ec03954f6141e3bf1a74a6f44
app.Run();





=======
app.Run();
>>>>>>> e4f0c2642b00fdb8eaf11ca7e3d59ede6e6b60e4
