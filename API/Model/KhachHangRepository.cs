using API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using API.Dto;

namespace API.Model
{

    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly AppDbContext _context;

        public KhachHangRepository(AppDbContext context)
        {
            _context = context;
        }

        // Thêm mới một khách hàng
        public async Task<tbKhachHang> AddKhachHang(tbKhachHang kh)
        {
            _context.KhachHang.Add(kh); // Thêm khách hàng vào DbSet KhachHang
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return kh; // Trả về khách hàng đã thêm
        }

        // Xóa khách hàng dựa trên mã khách hàng
        public async Task<bool> DeleteKhachHang(string maKhachHang)
        {
            var khachHang = await _context.KhachHang.FindAsync(maKhachHang); // Tìm khách hàng theo mã
            if (khachHang == null) return false; // Nếu không tìm thấy, trả về false

            _context.KhachHang.Remove(khachHang); // Xóa khách hàng khỏi DbSet KhachHang
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return true; // Trả về true nếu xóa thành công
        }

        // Lấy danh sách tất cả khách hàng
        public async Task<IEnumerable<tbKhachHang>> GetDanhSachKhachHang()
        {
            return await _context.KhachHang.ToListAsync(); // Trả về danh sách khách hàng
        }

        // Lấy thông tin khách hàng theo mã
        public async Task<tbKhachHang> GetKhachHangById(string maKhachHang)
        {
            return await _context.KhachHang.FindAsync(maKhachHang); // Tìm khách hàng theo mã và trả về
        }

        // Cập nhật thông tin khách hàng
        public async Task<tbKhachHang> UpdateKhachHang(tbKhachHang kh)
        {
            var existingKhachHang = await _context.KhachHang.FindAsync(kh.MaKH); // Tìm khách hàng theo mã
            if (existingKhachHang == null) return null; // Nếu không tìm thấy, trả về null

            // Cập nhật các thuộc tính của khách hàng
            existingKhachHang.HoTen = kh.HoTen;
            existingKhachHang.Sdt = kh.Sdt;
            existingKhachHang.NgaySinh = kh.NgaySinh;
            existingKhachHang.Email = kh.Email;
            existingKhachHang.TinhTrang = kh.TinhTrang;
            existingKhachHang.TenTK = kh.TenTK;
            existingKhachHang.MatKhau = kh.MatKhau;
            existingKhachHang.DiemThuong = kh.DiemThuong;
            existingKhachHang.SoLuongVoucher = kh.SoLuongVoucher;
            existingKhachHang.DiemDanh = kh.DiemDanh;
            existingKhachHang.NgayDiemDanhCuoi = kh.NgayDiemDanhCuoi;

            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return existingKhachHang; // Trả về khách hàng đã cập nhật
        }

        public async Task<tbKhachHang> Login(string email, string plainPassword)
        {
            // Tìm khách hàng dựa trên TenTK
            var khachHang = await _context.KhachHang
                .FirstOrDefaultAsync(kh => kh.TenTK == email);

            if (khachHang == null)
                return null; // Không tìm thấy người dùng

            // So sánh mật khẩu băm
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(plainPassword, khachHang.MatKhau);

            if (!isPasswordValid)
                return null; // Sai mật khẩu

            return khachHang; // Trả về thông tin khách hàng nếu đăng nhập thành công
        }

        public async Task<bool> RegisterAsync(tbKhachHang khachHang)
        {
            // Kiểm tra xem tài khoản hoặc email đã tồn tại
            var existingAccount = await _context.Set<tbKhachHang>()
                .FirstOrDefaultAsync(x => x.TenTK == khachHang.TenTK || x.Email == khachHang.Email);

            if (existingAccount != null)
            {
                return false; // Tài khoản hoặc email đã tồn tại
            }

            // Băm mật khẩu bằng BCrypt
            khachHang.MatKhau = BCrypt.Net.BCrypt.HashPassword(khachHang.MatKhau);

            // Thêm tài khoản mới vào cơ sở dữ liệu
            await _context.Set<tbKhachHang>().AddAsync(khachHang);
            await _context.SaveChangesAsync();

            return true; // Đăng ký thành công
        }

        [HttpGet("LichSu/{maKH}")]
        public async Task<IEnumerable<LichSuKhachHangDTO>> GetLSKhachHang(string maKH)
        {


            var result = await _context.LichSuKH
                .FromSqlInterpolated($"SELECT * FROM dbo.fLichSuKH({maKH})")
                .ToListAsync();
            return result;
        }


    }
}
