using API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Model
{
    public class NhanVienRepository : INhanVienRepository
    {
        private readonly AppDbContext _context;  // Khai báo DbContext để truy cập cơ sở dữ liệu

        // Constructor nhận vào AppDbContext thông qua Dependency Injection
        public NhanVienRepository(AppDbContext context)
        {
            _context = context;
        }

        // Thêm một nhân viên mới vào cơ sở dữ liệu
        public async Task<tbNhanVien> AddNhanVien(tbNhanVien nv)
        {
            _context.NhanVien.Add(nv);  // Thêm nhân viên vào DbSet NhanVien
            await _context.SaveChangesAsync();  // Lưu thay đổi vào cơ sở dữ liệu
            return nv;  // Trả về nhân viên đã thêm
        }

        // Cập nhật thông tin nhân viên dựa trên mã nhân viên
        public async Task<tbNhanVien> UpdateNhanVien(tbNhanVien nv)
        {
            var nhanVien = await _context.NhanVien.FindAsync(nv.MaNV);  // Tìm nhân viên theo mã
            if (nhanVien == null)
            {
                return null;  // Nếu không tìm thấy nhân viên, trả về null
            }

            // Cập nhật thông tin nhân viên
            nhanVien.HoTen = nv.HoTen;
            nhanVien.Sdt = nv.Sdt;
            nhanVien.GioiTinh = nv.GioiTinh;
            nhanVien.NgaySinh = nv.NgaySinh;
            nhanVien.DiaChi = nv.DiaChi;
            nhanVien.Cccd = nv.Cccd;
            nhanVien.TinhTrang = nv.TinhTrang;
            nhanVien.TenTK = nv.TenTK;
            nhanVien.MatKhau = nv.MatKhau;
            nhanVien.MaQL = nv.MaQL;

            await _context.SaveChangesAsync();  // Lưu thay đổi vào cơ sở dữ liệu
            return nhanVien;  // Trả về nhân viên đã cập nhật
        }

        // Xóa nhân viên khỏi cơ sở dữ liệu dựa trên mã nhân viên
        public async Task<bool> DeleteNhanVien(string maNhanVien)
        {
            var nhanVien = await _context.NhanVien.FindAsync(maNhanVien);  // Tìm nhân viên theo mã
            if (nhanVien == null)
            {
                return false;  // Nếu không tìm thấy nhân viên, trả về false
            }

            _context.NhanVien.Remove(nhanVien);  // Xóa nhân viên khỏi cơ sở dữ liệu
            await _context.SaveChangesAsync();  // Lưu thay đổi vào cơ sở dữ liệu
            return true;  // Trả về true nếu xóa thành công
        }

        // Lấy tất cả nhân viên từ cơ sở dữ liệu
        public async Task<IEnumerable<tbNhanVien>> GetAllNhanVien()
        {
            return await _context.NhanVien.ToListAsync();  // Lấy tất cả nhân viên từ cơ sở dữ liệu
        }

        // Lấy thông tin nhân viên theo mã nhân viên
        public async Task<tbNhanVien> GetNhanVienById(string maNhanVien)
        {
            var nhanVien = await _context.NhanVien
                .FirstOrDefaultAsync(nv => nv.MaNV == maNhanVien);  // Tìm nhân viên theo mã
            return nhanVien;  // Trả về thông tin nhân viên nếu tìm thấy, ngược lại trả về null
        }

        // Lấy tên nhân viên theo mã nhân viên
        public async Task<string> GetNhanVien(string maNhanVien)
        {
            var nhanVien = await _context.NhanVien
                .FirstOrDefaultAsync(nv => nv.MaNV == maNhanVien);  // Tìm nhân viên theo mã
            if (nhanVien != null)
            {
                return nhanVien.HoTen;  // Trả về tên của nhân viên
            }

            return "Nhân viên không tồn tại";  // Nếu không tìm thấy nhân viên, trả về thông báo
        }
    }
}
