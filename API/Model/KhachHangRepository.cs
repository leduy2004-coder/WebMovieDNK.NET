using API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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
    }
}
