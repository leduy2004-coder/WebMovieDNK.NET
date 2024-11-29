using API.Model;
using CloudinaryDotNet;
using Microsoft.EntityFrameworkCore;

using System;

var builder = WebApplication.CreateBuilder(args);

// 1. Cấu hình DbContext
builder.Services.AddDbContextPool<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

// 2. Thêm các repository với Dependency Injection
builder.Services.AddTransient<IPhimRepository, PhimRepository>();
builder.Services.AddTransient<ISuatChieuRepository, SuatChieuRepository>();
builder.Services.AddTransient<IBinhLuanRepository, BinhLuanRepository>();
builder.Services.AddTransient<IKhachHangRepository, KhachHangRepository>();
builder.Services.AddTransient<INhanVienRepository, NhanVienRepository>();
builder.Services.AddTransient<IVeRepository, VeRepository>();
builder.Services.AddTransient<IQuanLiRepository, QuanLiRepository>();
builder.Services.AddTransient<IPhongChieuRepository, PhongChieuRepository>();

// 3. Thêm Controllers
builder.Services.AddControllers();


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

app.Run();
