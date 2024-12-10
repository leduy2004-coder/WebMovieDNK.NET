using API.Model;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;

using System;

var builder = WebApplication.CreateBuilder(args);
// Cấu hình CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost7226", builder =>
    {
        builder.WithOrigins("https://localhost:7226")  // Cho phép truy cập từ localhost:7226
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
// Cấu hình DbContext
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

// Thêm các repository với Dependency Injection
builder.Services.AddTransient<IPhimRepository, PhimRepository>();
builder.Services.AddTransient<ISuatChieuRepository, SuatChieuRepository>();
builder.Services.AddTransient<IBinhLuanRepository, BinhLuanRepository>();
builder.Services.AddTransient<IKhachHangRepository, KhachHangRepository>();
builder.Services.AddTransient<INhanVienRepository, NhanVienRepository>();
builder.Services.AddTransient<IVeRepository, VeRepository>();
builder.Services.AddTransient<IDatVeRepository, DatVeRepository>();
builder.Services.AddTransient<IDatDoUongRepository, DatDoUongRepository>();
builder.Services.AddTransient<IUuDaiRepository, UuDaiRepository>();
builder.Services.AddSingleton<CloudinaryService>();

//  Thêm Controllers
builder.Services.AddControllers();

builder.Services.AddMemoryCache();

builder.Services.Configure<CloudinarySettings>(builder.Configuration.GetSection("CloudinarySettings"));

// Cấu hình Cloudinary
builder.Services.AddSingleton(x =>
{
    var config = builder.Configuration.GetSection("CloudinarySettings").Get<CloudinarySettings>();
    var account = new Account(config.CloudName, config.ApiKey, config.ApiSecret);
    return new Cloudinary(account);
});


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "KTCuoiKi",
        Version = "v1"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.UseCors("AllowLocalhost7226");
app.Run();
