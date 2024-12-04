using System.Net.Http.Headers;
using Web.Api;
using WEB.Api;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Cấu hình HttpClient
builder.Services.AddHttpClient<ApiService>(client =>
{
    client.BaseAddress = new Uri("https://localhost:7079/");
    client.DefaultRequestHeaders.Accept.Clear();
    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSession(options =>
{
    //options.IdleTimeout = TimeSpan.FromMinutes(30); // Thời gian timeout
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// Thêm các service khác vào Dependency Injection
//builder.Services.AddScoped<PhimService>();
builder.Services.AddScoped<NhanVienService>();
builder.Services.AddScoped<Admin_SuatChieuService>();
builder.Services.AddScoped<Admin_ThongKeService>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<LoginService>();
//builder.Services.AddScoped<KhachHangService>();
builder.Services.AddScoped<Admin_QLKhachHangService>();

builder.Services.AddScoped<ScheduleService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<CommentService>();
builder.Services.AddScoped<OrderDrinkService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseSession();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
