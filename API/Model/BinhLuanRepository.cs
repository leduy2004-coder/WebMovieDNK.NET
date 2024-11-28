using API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class BinhLuanRepository : IBinhLuanRepository
    {
        private readonly AppDbContext _context;

        public BinhLuanRepository(AppDbContext context)
        {
            _context = context;
        }

        // Thêm bình luận
        public async Task<tbBinhLuan> AddBinhLuan(tbBinhLuan bl)
        {
            _context.BinhLuan.Add(bl); // Thêm bình luận vào DbSet BinhLuan
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return bl; // Trả về bình luận đã thêm
        }

        // Cập nhật bình luận
        public async Task<tbBinhLuan> UpdateBinhLuan(tbBinhLuan bl)
        {
            var existingBinhLuan = await _context.BinhLuan.FindAsync(bl.MaBinhLuan);
            if (existingBinhLuan == null)
            {
                return null; // Nếu không tìm thấy bình luận, trả về null
            }

            // Cập nhật nội dung
            existingBinhLuan.NoiDung = bl.NoiDung;
            existingBinhLuan.Gio = bl.Gio;
            existingBinhLuan.TinhTrang = bl.TinhTrang;

            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return existingBinhLuan; // Trả về bình luận đã cập nhật
        }

        // Xóa bình luận
        public async Task<bool> DeleteBinhLuan(string maBinhLuan)
        {
            var existingBinhLuan = await _context.BinhLuan.FindAsync(int.Parse(maBinhLuan));
            if (existingBinhLuan == null)
            {
                return false; // Nếu không tìm thấy bình luận, trả về false
            }

            _context.BinhLuan.Remove(existingBinhLuan); // Xóa bình luận khỏi DbSet
            await _context.SaveChangesAsync(); // Lưu thay đổi vào cơ sở dữ liệu
            return true; // Trả về true nếu xóa thành công
        }

        public async Task<IEnumerable<tbBinhLuan>> GetAllBinhLuan(string maPhim)
        {
            if (string.IsNullOrEmpty(maPhim))
            {
                return Enumerable.Empty<tbBinhLuan>(); // Trả về danh sách rỗng nếu mã phim không hợp lệ
            }

            return await _context.BinhLuan
                                 .Where(bl => bl.MaPhim == maPhim && bl.TinhTrang) // Lọc theo mã phim và tình trạng
                                 .ToListAsync();
        }




        // Lấy bình luận theo mã
        public async Task<tbBinhLuan> GetBinhLuan(string maPhim)
        {
            return await _context.BinhLuan
                .Include(bl => bl.Phim)
                .Include(bl => bl.KhachHang)
                .FirstOrDefaultAsync(bl => bl.MaBinhLuan == int.Parse(maPhim));
        }

    
    }
}
