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
        public async Task<IEnumerable<tbSuatChieu>> GetSuatChieuTheoPhim(string maPhim)
        {
            return await _context.SuatChieu
                .Where(sc => sc.MaPhim == maPhim) // Điều kiện lọc: Suất chiếu của phim theo mã phim
                .ToListAsync(); // Thực hiện truy vấn và trả về danh sách suất chiếu theo mã phim
        }


        
    }
}
