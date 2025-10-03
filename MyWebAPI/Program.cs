<<<<<<< HEAD
﻿//using Microsoft.AspNetCore.Authentication.JwtBearer;
=======
﻿using Microsoft.AspNetCore.Authentication.JwtBearer;
>>>>>>> c214ea77ef1a2505f4c00b5dc42546b0ca6ba474
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger + bearer support
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MyWebAPI", Version = "v1" });
    var scheme = new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "Nhập token dạng: Bearer {token}"
    };
    c.AddSecurityDefinition("Bearer", scheme);
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        [scheme] = new List<string>()
    });
});

// Ocelot
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);
builder.Services.AddOcelot(builder.Configuration);

// CORS
builder.Services.AddCors(o => o.AddPolicy("AllowAll",
    p => p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));

// ===== JWT =====
<<<<<<< HEAD
//var jwt = builder.Configuration.GetSection("Jwt");
//var keyBytes = Encoding.UTF8.GetBytes(jwt["Key"]!);

//builder.Services
//    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(o =>
//    {
//        o.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = jwt["Issuer"],
//            ValidAudience = jwt["Audience"],
//            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
//            ClockSkew = TimeSpan.Zero
//        };
//    });
=======
var jwt = builder.Configuration.GetSection("Jwt");
var keyBytes = Encoding.UTF8.GetBytes(jwt["Key"]!);

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwt["Issuer"],
            ValidAudience = jwt["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            ClockSkew = TimeSpan.Zero
        };
    });
>>>>>>> c214ea77ef1a2505f4c00b5dc42546b0ca6ba474

builder.Services.AddAuthorization();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowAll");

app.UseAuthentication();   // << phải trước UseAuthorization
app.UseAuthorization();
<<<<<<< HEAD

app.MapGet("/", () => "Gateway OK");
//app.MapControllers();      // << bật vì bạn có DangNhapController trong MyWebAPI

await app.UseOcelot();     // << để cuối
=======
>>>>>>> c214ea77ef1a2505f4c00b5dc42546b0ca6ba474

app.MapGet("/", () => "Gateway OK");
//app.MapControllers();      // << bật vì bạn có DangNhapController trong MyWebAPI

await app.UseOcelot();     // << để cuối

app.Run();
