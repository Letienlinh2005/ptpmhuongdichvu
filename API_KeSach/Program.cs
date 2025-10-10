using MyWebAPI.BLL.Services;
using MyWebAPI.DAL.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")!;

builder.Services.AddScoped<IKeSachRepository>(provider =>
    new KeSachRepository(connectionString));

builder.Services.AddScoped<IKeSachService, KeSachService>();

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