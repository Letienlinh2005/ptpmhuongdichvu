using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyWebAPI", Version = "v1" });
});

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

// Thứ tự middleware rất quan trọng
app.UseHttpsRedirection();
app.UseStaticFiles();           // Serve static files từ wwwroot
app.UseRouting();
app.UseCors("AllowAll");        // Nếu cần CORS
app.UseAuthorization();
app.MapControllers();           // Map API controllers

// Fallback phải đặt cuối cùng
app.MapFallbackToFile("index.html");

app.Run();