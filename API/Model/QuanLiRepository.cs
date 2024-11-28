using API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class QuanLiRepository : IQuanLiRepository
    {
        private readonly AppDbContext _context;

        // Constructor nhận vào DbContext để tương tác với cơ sở dữ liệu
        public QuanLiRepository(AppDbContext context)
        {
            _context = context;
        }

        // Thêm mới quản lý
        public async Task<tbQuanLi> AddQuanLi(tbQuanLi ql)
        {
            // Thêm quản lý vào DbSet tbQuanLi
            _context.QuanLi.Add(ql);
            await _context.SaveChangesAsync();  // Lưu thay đổi vào cơ sở dữ liệu
            return ql;  // Trả về quản lý vừa thêm
        }

        // Xóa quản lý
        public async Task<bool> DeleteQuanli(string maQuanLy)
        {
            var quanLy = await _context.QuanLi.FindAsync(maQuanLy);  // Tìm quản lý theo mã
            if (quanLy == null)
            {
                return false;  // Nếu không tìm thấy quản lý, trả về false
            }

            _context.QuanLi.Remove(quanLy);  // Xóa quản lý khỏi cơ sở dữ liệu
            await _context.SaveChangesAsync();  // Lưu thay đổi vào cơ sở dữ liệu
            return true;  // Trả về true nếu xóa thành công
        }

        // Lấy danh sách tất cả quản lý
        public async Task<IEnumerable<tbQuanLi>> GetDanhSachQuanLi()
        {
            // Trả về tất cả các quản lý từ cơ sở dữ liệu
            return await _context.QuanLi.ToListAsync();
        }

        // Cập nhật thông tin quản lý
        public async Task<tbQuanLi> UpdateQuanLi(tbQuanLi ql)
        {
            var quanLy = await _context.QuanLi.FindAsync(ql.MaQL);  // Tìm quản lý theo mã
            if (quanLy == null)
            {
                return null;  // Nếu không tìm thấy quản lý, trả về null
            }

            // Cập nhật thông tin quản lý
            quanLy.HoTen = ql.HoTen;
            quanLy.Sdt = ql.Sdt;
            quanLy.GioiTinh = ql.GioiTinh;
            quanLy.NgaySinh = ql.NgaySinh;
            quanLy.DiaChi = ql.DiaChi;
            quanLy.Cccd = ql.Cccd;
            quanLy.TinhTrang = ql.TinhTrang;
            quanLy.TenTK = ql.TenTK;
            quanLy.MatKhau = ql.MatKhau;
            quanLy.Sdt_Check = ql.Sdt_Check;

            await _context.SaveChangesAsync();  // Lưu thay đổi vào cơ sở dữ liệu
            return quanLy;  // Trả về quản lý đã cập nhật
        }
    }
}
