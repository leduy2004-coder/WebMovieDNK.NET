
using API.Data;
using Microsoft.EntityFrameworkCore;


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
        public DbSet<tbPhongChieu> PhongChieu { get; set; }
        public DbSet<tbCaChieu> CaChieu { get; set; }
        public DbSet<tbSuatChieu> SuatChieu { get; set; }
        public DbSet<tbGhe> Ghe { get; set; }
        public DbSet<tbVe> Ve { get; set; }
        public DbSet<tbBookVe> BookVe { get; set; }
        public DbSet<tbBookGhe> BookGhe { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Định nghĩa index
            modelBuilder.Entity<tbNhanVien>()
                .HasIndex(nv => new { nv.TenTK, nv.MatKhau })
                .IsUnique()
                .HasDatabaseName("U_nv_user");

            modelBuilder.Entity<tbNhanVien>()
                .HasIndex(nv => nv.Sdt)
                .IsUnique()
                .HasDatabaseName("U_nv_sdt");

            modelBuilder.Entity<tbNhanVien>()
                .HasIndex(nv => nv.Cccd)
                .IsUnique()
                .HasDatabaseName("U_nv_cccd");

            // Cấu hình khóa chính hợp thành cho tbBookGhe
            modelBuilder.Entity<tbBookGhe>()
                .HasKey(b => new { b.MaGhe, b.MaBook }); // Chỉ định khóa chính hợp thành từ MaGhe và MaBook
        }

    }
}

