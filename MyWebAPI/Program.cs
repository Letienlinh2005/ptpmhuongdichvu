using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyWebAPI", Version = "v1" });
});

// Đọc ocelot.json (hot reload khi DEV)
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

var ocelotPath = Path.Combine(builder.Environment.ContentRootPath, "ocelot.json");
Console.WriteLine($"[Gateway] Ocelot config path = {ocelotPath} | Exists = {File.Exists(ocelotPath)}");

builder.Services.AddOcelot(builder.Configuration);

// Thêm CORS nếu frontend và backend khác domain
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MyWebAPI v1");
    });
}


app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("AllowAll");        
app.MapGet("/", () => "Gateway OK");

await app.UseOcelot();

app.Run();