﻿using API.Data;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Globalization;

namespace API.Model
{
    public class PhimRepository : IPhimRepository
    {
        private readonly AppDbContext _context;

        public PhimRepository(AppDbContext context)
        {
            _context = context;
        }

        // Lấy danh sách phim đang chiếu
        public async Task<IEnumerable<tbPhim>> GetPhimDangChieu()
        {
            // Lấy danh sách mã phim từ thủ tục
            var movieIds = await _context.Set<GetPhimDangChieu>()
                .FromSqlRaw("EXEC GetSuatChieu")
                .ToListAsync();

            // Chuyển sang bộ nhớ và lấy chỉ mã phim
            var movieIdList = movieIds.Select(m => m.MaPhim).ToList();

            // Dùng danh sách mã phim để lấy chi tiết phim
            var movies = await _context.Phim
                .Where(p => movieIdList.Contains(p.MaPhim))
                .ToListAsync();

            return movies;
        }

        // Lấy tất cả phim
        public async Task<IEnumerable<tbPhim>> GetPHIMs()
        {
            try
            {
                var danhSachPhim = await _context.Phim
                .Include(p => p.TheLoaiPhim) // Eager Loading
                .ToListAsync();
                return danhSachPhim;
            }catch(Exception e)
            {
                return null;
            }

        }

        // Lấy tất cả phim co tinh trang 1
        public async Task<IEnumerable<tbPhim>> GetPHIMStatus()
        {
            try
            {
                var danhSachPhim = await _context.Phim
                    .Where(p => p.TinhTrang == true)
                    .ToListAsync();
                return danhSachPhim;
            }
            catch (Exception e)
            {
                return null;
            }

        }
        // Lấy tất cả loai phim
        public async Task<IEnumerable<tbTheLoaiPhim>> GetTheLoaiPHIMs()
        {
            return await _context.TheLoaiPhim.ToListAsync();
        }
        // Lấy chi tiết của một phim dựa trên mã phim
        public async Task<tbPhim> GetThongTinPhim(string maPhim)
        {
            return await _context.Phim
                .FirstOrDefaultAsync(p => p.MaPhim == maPhim);
        }

        // Lấy danh sách phim chưa chiếu
        public async Task<IEnumerable<tbPhim>> GetPhimChuaChieu()
        {
            // Gọi stored procedure trả về danh sách mã phim
            var movieIds = await _context.Set<GetPhimDangChieu>()
                .FromSqlRaw("EXEC GetSuatChuaChieu")
                .ToListAsync();

            // Chuyển sang bộ nhớ và lấy chỉ mã phim
            var movieIdList = movieIds.Select(m => m.MaPhim).ToList();

            // Dùng mã phim để lấy chi tiết phim
            var movies = await _context.Phim
                .Where(p => movieIdList.Contains(p.MaPhim))
                .ToListAsync();

            return movies;
        }


        // Xóa một bộ phim dựa trên mã phim
        public async Task<bool> DeletePHIM(string maPhim)
        {
            var phim = await _context.Phim.FirstOrDefaultAsync(p => p.MaPhim == maPhim);
            if (phim == null) return false;

            _context.Phim.Remove(phim);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<tbPhim> AddPHIM(tbPhim phim)
        {
            try
            {
                phim.MaPhim = "";
                await _context.Phim.AddAsync(phim);

                await _context.SaveChangesAsync();

                return phim;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi thêm phim.", ex);
            }
        }

        public async Task<tbPhim> UpdatePHIM(tbPhim phim)
        {
            try
            {
                var existingPhim = await _context.Phim.FirstOrDefaultAsync(p => p.MaPhim == phim.MaPhim);

                if (existingPhim == null)
                {
                    throw new KeyNotFoundException("Không tìm thấy bộ phim với mã phim: " + phim.MaPhim);
                }
                existingPhim.TenPhim = phim.TenPhim;
                existingPhim.DaoDien = phim.DaoDien;
                existingPhim.DoTuoiYeuCau = phim.DoTuoiYeuCau;
                existingPhim.NgayKhoiChieu = phim.NgayKhoiChieu;
                existingPhim.ThoiLuong = phim.ThoiLuong;
                existingPhim.TinhTrang = phim.TinhTrang;
                existingPhim.HinhDaiDien = phim.HinhDaiDien;
                existingPhim.Video = phim.Video;
                existingPhim.MoTa = phim.MoTa;
                existingPhim.MaLPhim = phim.MaLPhim;

                await _context.SaveChangesAsync();

                return phim;
            }
            catch (Exception ex)
            {
                throw new Exception("Có lỗi xảy ra khi cập nhật phim.", ex);
            }
        }

        public async Task<IEnumerable<tbPhim>> GetTimPhim(string tenPhim)
        {
            if (string.IsNullOrEmpty(tenPhim))
            {
                return Enumerable.Empty<tbPhim>(); 
            }

            var result = await _context.Phim
                            .Include(p => p.TheLoaiPhim)
                                       .Where(p => p.TenPhim.Contains(tenPhim)) 
                                       .ToListAsync();
    
            return result;
        }

    }
}
