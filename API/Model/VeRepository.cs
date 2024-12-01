using API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Model
{
    public class VeRepository : IVeRepository
    {
        private readonly AppDbContext _context;

        public VeRepository(AppDbContext context)
        {
            _context = context;
        }

        // Thêm vé
        public async Task<tbVe> AddVe(tbVe v)
        {
            _context.Ve.Add(v);
            await _context.SaveChangesAsync();
            return v;
        }

        // Cập nhật vé
        public async Task<tbVe> UpdateVe(tbVe v)
        {
            var existingVe = await _context.Ve.FirstOrDefaultAsync(x => x.MaVe == v.MaVe);
            if (existingVe == null)
            {
                return null; // Không tìm thấy vé
            }

            // Cập nhật các thuộc tính của vé
            existingVe.MaPhim = v.MaPhim;
            existingVe.MaNV = v.MaNV;
            existingVe.TinhTrang = v.TinhTrang;
            existingVe.SoLuongToiDa = v.SoLuongToiDa;
            existingVe.SoLuongDaBan = v.SoLuongDaBan;
            existingVe.Tien = v.Tien;

            await _context.SaveChangesAsync();
            return existingVe;
        }

        // Xóa vé
        public async Task<bool> DeleteVe(string maVe)
        {
            var ve = await _context.Ve.FirstOrDefaultAsync(x => x.MaVe == maVe);
            if (ve == null)
            {
                return false; // Không tìm thấy vé
            }

            _context.Ve.Remove(ve);
            await _context.SaveChangesAsync();
            return true;
        }

        // Lấy danh sách vé
        public async Task<IEnumerable<tbVe>> GetDanhSachVe()
        {
            return await _context.Ve.Include(v => v.Phim).Include(v => v.NhanVien).ToListAsync();
        }

        // Lấy chi tiết của một ve dựa trên mã phim
        public async Task<tbVe> GetThongTinVe(string maPhim)
        {
            return await _context.Ve
                .FirstOrDefaultAsync(p => p.MaPhim == maPhim);
        }
    }
}
