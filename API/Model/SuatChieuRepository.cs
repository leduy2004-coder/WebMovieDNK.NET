using API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class SuatChieuRepository : ISuatChieuRepository
    {
        private readonly AppDbContext _context; // Khai báo DbContext để truy cập cơ sở dữ liệu

        // Constructor nhận vào ApplicationDbContext thông qua Dependency Injection
        public SuatChieuRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<tbSuatChieu>> GetSuatChuaChieu(string maPhim)
        {
            return await _context.SuatChieu
                .Where(sc => sc.TinhTrang == false && sc.MaPhim == maPhim) // Điều kiện lọc: Suất chiếu chưa chiếu và mã phim
                .ToListAsync(); // Thực hiện truy vấn và trả về danh sách suất chiếu chưa chiếu
        }

        // Phương thức lấy tất cả suất chiếu theo mã phim
        public async Task<IEnumerable<DateTime>> GetNgayChieuTheoPhim(string maPhim)
        {
            // Gọi SQL function fXuatNgayChieu với tham số là mã phim
            var ngayChieuList = await _context.Set<tbMaPhim>()
                .FromSqlRaw("SELECT * FROM dbo.fXuatNgayChieu({0})", maPhim)
                .ToListAsync();

            // Trả về danh sách các ngày chiếu
            return ngayChieuList.Select(n => n.NgayChieu);
        }

        public Task<IEnumerable<tbSuatChieu>> GetSuatChieuTheoPhim(string maPhim)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<tbCaChieu>> GetCaChieuTheoPhimVaNgay(string maPhim, DateTime ngayChieu)
        {
            // Gọi SQL function fXuatThoiGianChieu với tham số là mã phim và ngày chiếu
            var caChieuList = await _context.Set<tbCaChieu>()
                .FromSqlRaw("SELECT * FROM dbo.fXuatThoiGianChieu({0}, {1})", maPhim, ngayChieu)
                .ToListAsync();

            // Trả về danh sách các suất chiếu
            return caChieuList;
        }

        


        public async Task<bool> deleteSuatChieu(string maSuat)
        {
            var suatChieu = await _context.SuatChieu.FindAsync(maSuat);
            if (suatChieu == null)
            {
                return false;  // Nếu không tìm thấy suất chiếu, trả về false
            }

            _context.SuatChieu.Remove(suatChieu);  // Xóa suất chiếu khỏi cơ sở dữ liệu
            await _context.SaveChangesAsync();  // Lưu thay đổi vào cơ sở dữ liệu
            return true;  // Trả về true nếu xóa thành công
        }

        public Task<tbSuatChieu> addSuatChieu(string maSuat)
        {
            throw new NotImplementedException();
        }

        public Task<tbSuatChieu> upadteSuatChieu(string maSuat)
        {
            throw new NotImplementedException();
        }
    }
}
