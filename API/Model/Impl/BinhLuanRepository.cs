using API.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model.Impl
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
            _context.BinhLuan.Add(bl);
            await _context.SaveChangesAsync();
            return bl;
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
                return Enumerable.Empty<tbBinhLuan>();
            }

            return await _context.BinhLuan
                                 .Where(bl => bl.MaPhim == maPhim && bl.TinhTrang)
                                 .ToListAsync();
        }

        // Lấy bình luận theo mã
        public async Task<tbBinhLuan> GetBinhLuan(string maPhim)
        {
            return await _context.BinhLuan
                .Include(bl => bl.MaPhim)
                .Include(bl => bl.MaKH)
                .FirstOrDefaultAsync(bl => bl.MaBinhLuan == int.Parse(maPhim));
        }


    }
}
