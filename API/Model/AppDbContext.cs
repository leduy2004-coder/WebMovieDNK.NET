
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
        public DbSet<tbTheLoaiPhim> TheLoaiPhim { get; set; }
        public DbSet<tbBinhLuan> BinhLuan { get; set; }
        public DbSet<tbCaChieu> CaChieu { get; set; }
        public DbSet<tbSuatChieu> SuatChieu { get; set; }
        public DbSet<tbGhe> Ghe { get; set; }
        public DbSet<tbVe> Ve { get; set; }
        public DbSet<tbBookVe> BookVe { get; set; }
        public DbSet<tbBookGhe> BookGhe { get; set; }
        public DbSet<GetPhimDangChieu> getPhimDangChieuDTOs { get; set; }
        public DbSet<ThongKeVe> ThongKeVe { get; set; }

        public DbSet<GetNgayChieu> getNgayChieus { get; set; }
        public DbSet<GetCaChieu> getCaChieus { get; set; }
        public DbSet<tbUuDai> UuDai { get; set; }
        public DbSet<KhachHangUuDai> KhachHangUuDai { get; set; }
        public DbSet<tbDoUong> DoUong { get; set; }
        public DbSet<tbBookDoUong> BookDoUong { get; set; }
        public DbSet<tbPhongChieu> PhongChieu { get; set; }
        public DbSet<LichSuKhachHangDTO> LichSuKH { get; set; }
        public DbSet<DoanhThuTrongNamDTO> doanhThuTrongNamDTO { get; set; }
        public DbSet<TopCustomerDTO> topCustomerDTO { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Cấu hình khóa chính hợp thành cho tbBookGhe
            modelBuilder.Entity<tbBookGhe>()
                .HasKey(b => new { b.MaGhe, b.MaBook }); // Chỉ định khóa chính hợp thành từ MaGhe và MaBook
            modelBuilder.Entity<tbBookDoUong>()
                .HasKey(b => new { b.MaBook, b.MaDoUong });

            modelBuilder.Entity<GetPhimDangChieu>().HasNoKey();
            modelBuilder.Entity<GetNgayChieu>().HasNoKey();
            modelBuilder.Entity<LichSuKhachHangDTO>().HasNoKey();
            modelBuilder.Entity<ThongKeVe>().HasNoKey();
            modelBuilder.Entity<DoanhThuTrongNamDTO>().HasNoKey();
            modelBuilder.Entity<TopCustomerDTO>().HasNoKey();


            modelBuilder.Entity<KhachHangUuDai>()
               .HasKey(kh => new { kh.MaKH, kh.MaUuDai });
        }

    }
}

