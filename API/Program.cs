using MyWebAPI.BLL.Services;
using MyWebAPI.DAL.Repositories;
using static MyWebAPI.DAL.Repositories.PhieuMuonDAL;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.using static MyWebAPI.DAL.Repositories.PhieuMuonDAL;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
builder.Services.AddScoped<IPhieuMuonRepository>(provider =>
    new PhieuMuonRepository(connectionString));
builder.Services.AddScoped<IPhieuMuonService, PhieuMuonService>();

builder.Services.AddScoped<IBanDocRepository>(provider =>
    new BanDocRepository(connectionString));
builder.Services.AddScoped<IBanDocService, BanDocService>();

builder.Services.AddScoped<ISachRepository>(provider =>
    new SachRepository(connectionString));
builder.Services.AddScoped<ISachService, SachService>();

builder.Services.AddScoped<IBanSaoRepository>(provider =>
    new BanSaoRepository(connectionString));
builder.Services.AddScoped<IBanSaoService, BanSaoService>();

builder.Services.AddScoped<ITaiKhoanRepository>(provider =>
    new TaiKhoanRepository(connectionString));
builder.Services.AddScoped<ITaiKhoanService, TaiKhoanService>();

builder.Services.AddScoped<ITheLoaiRepository>(provider =>
    new TheLoaiRepository(connectionString));
builder.Services.AddScoped<ITheLoaiService, TheLoaiService>();

builder.Services.AddScoped<IKeSachRepository>(provider =>
    new KeSachRepository(connectionString));
builder.Services.AddScoped<IKeSachService, KeSachService>();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
