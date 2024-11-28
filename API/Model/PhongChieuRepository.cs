using API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class PhongChieuRepository : IPhongChieuRepository
    {
        private readonly AppDbContext _context;

        public PhongChieuRepository(AppDbContext context)
        {
            _context = context;
        }

        // Lấy tất cả phòng chiếu
        public async Task<IEnumerable<tbPhongChieu>> GetDanhSachPhongChieu()
        {
            return await _context.PhongChieu.ToListAsync();
        }

        // Lấy thông tin phòng chiếu (phòng chiếu mặc định hoặc phòng chiếu theo điều kiện)
        public async Task<tbPhongChieu> GetPhongChieu()
        {
            // Nếu cần lọc theo điều kiện, có thể thay đổi query dưới đây.
            return await _context.PhongChieu.FirstOrDefaultAsync();
        }

        // Thêm phòng chiếu mới
        public async Task<tbPhongChieu> AddPhongChieu(tbPhongChieu pc)
        {
            if (pc == null)
            {
                throw new ArgumentNullException(nameof(pc), "Phòng chiếu không hợp lệ.");
            }

            await _context.PhongChieu.AddAsync(pc);
            await _context.SaveChangesAsync();
            return pc;
        }

        // Cập nhật phòng chiếu
        public async Task<tbPhongChieu> UpdatePhongChieu(tbPhongChieu pc)
        {
            var existingPhongChieu = await _context.PhongChieu.FindAsync(pc.MaPhong);
            if (existingPhongChieu == null)
            {
                return null; // Không tìm thấy phòng chiếu cần cập nhật
            }

            existingPhongChieu.TongSoGhe = pc.TongSoGhe;
            existingPhongChieu.LoaiMayChieu = pc.LoaiMayChieu;
            existingPhongChieu.LoaiAmThanh = pc.LoaiAmThanh;
            existingPhongChieu.DienTich = pc.DienTich;
            existingPhongChieu.TinhTrang = pc.TinhTrang;

            await _context.SaveChangesAsync();
            return existingPhongChieu;
        }

        // Xóa phòng chiếu theo mã phòng
        public async Task<bool> DeletePhongChieu(string maPhong)
        {
            var phongChieuToDelete = await _context.PhongChieu.FindAsync(maPhong);
            if (phongChieuToDelete == null)
            {
                return false; // Không tìm thấy phòng chiếu cần xóa
            }

            _context.PhongChieu.Remove(phongChieuToDelete);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
