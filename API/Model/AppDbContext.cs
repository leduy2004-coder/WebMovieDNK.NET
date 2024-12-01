
using API.Data;
using API.Dto;
using Microsoft.EntityFrameworkCore;
using static API.Model.SuatChieuRepository;


namespace API.Model
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<tbNhanVien> NhanVien { get; set; }

        public DbSet<tbPhim> Phim { get; set; }

        public DbSet<tbKhachHang> KhachHang { get; set; }
        public DbSet<tbQuanLi> QuanLi { get; set; }
        public DbSet<tbTheLoaiPhim> TheLoaiPhim { get; set; }
        public DbSet<tbBinhLuan> BinhLuan { get; set; }
        public DbSet<tbCaChieu> CaChieu { get; set; }
        public DbSet<tbSuatChieu> SuatChieu { get; set; }
        public DbSet<tbGhe> Ghe { get; set; }
        public DbSet<tbVe> Ve { get; set; }
        public DbSet<tbBookVe> BookVe { get; set; }
        public DbSet<tbBookGhe> BookGhe { get; set; }
        public DbSet<GetPhimDangChieu> getPhimDangChieuDTOs { get; set; }
        public DbSet<GetNgayChieu> getNgayChieus { get; set; }
        public DbSet<GetCaChieu> getCaChieus { get; set; }

        public DbSet<LichSuKhachHangDTO> LichSuKH { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình khóa chính hợp thành cho tbBookGhe
            modelBuilder.Entity<tbBookGhe>()
                .HasKey(b => new { b.MaGhe, b.MaBook }); // Chỉ định khóa chính hợp thành từ MaGhe và MaBook

            modelBuilder.Entity<GetPhimDangChieu>().HasNoKey();
            modelBuilder.Entity<GetNgayChieu>().HasNoKey();
   
        }

    }
}

