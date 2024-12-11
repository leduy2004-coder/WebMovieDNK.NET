using API.Data;
using API.Dto;
using KTGiuaKi.Dto;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Model.Impl
{

    public class DatVeRepository : IDatVeRepository
    {
        private readonly AppDbContext _context;

        public DatVeRepository(AppDbContext context)
        {
            _context = context;
        }

        // Thêm mới BookVe
        public async Task<tbBookVe> ThemMoiBookVe(tbBookVe bookVe)
        {
            // Lấy tất cả bản ghi từ cơ sở dữ liệu có MaBook bắt đầu bằng "BV"
            var lastBookVe = _context.BookVe
                .Where(b => b.MaBook != null && b.MaBook.StartsWith("BV"))
                .AsEnumerable() // Chuyển đổi dữ liệu sang bộ nhớ
                .OrderByDescending(b => int.Parse(b.MaBook.Substring(2))) // Sắp xếp theo số
                .FirstOrDefault(); // Sử dụng phiên bản đồng bộ cho dữ liệu trong bộ nhớ

            // Tạo mã mới
            string newMaBook = "BV" + (lastBookVe == null
                ? 1
                : int.Parse(lastBookVe.MaBook.Substring(2)) + 1);

            bookVe.MaBook = newMaBook;

            // Thêm vào cơ sở dữ liệu
            _context.BookVe.Add(bookVe);
            await _context.SaveChangesAsync();

            return bookVe; // Trả về đối tượng sau khi thêm
        }



        // Thêm mới BookGhe
        public async Task<tbBookGhe> ThemMoiBookGhe(tbBookGhe bookGhe)
        {
            await _context.BookGhe.AddAsync(bookGhe);
            await _context.SaveChangesAsync();
            return bookGhe;
        }

        public async Task<DatVeThanhCongDTO> LayThongTinDat(string maBook)
        {
            var bookVe = await _context.BookVe
                .FirstOrDefaultAsync(ve => ve.MaBook == maBook);
            if (bookVe == null)
            {
                throw new Exception("Không tìm thấy thông tin đặt vé.");
            }
            var khachHang = await _context.KhachHang
                .FirstOrDefaultAsync(kh => kh.MaKH == bookVe.MaKH);
            if (bookVe == null)
            {
                throw new Exception("Không tìm thấy thông tin đặt vé.");
            }
            var suatChieu = await _context.SuatChieu
                .FirstOrDefaultAsync(suat => suat.MaSuat == bookVe.MaSuat);
            if (suatChieu == null)
            {
                throw new Exception("Không tìm thấy thông tin suất chiếu.");
            }

            var phim = await _context.Phim
                .FirstOrDefaultAsync(p => p.MaPhim == suatChieu.MaPhim);
            if (phim == null)
            {
                throw new Exception("Không tìm thấy thông tin phim.");
            }
            var gheDat = await _context.BookGhe
                .Where(ghe => ghe.MaBook == maBook)
                .ToListAsync();

            // Ghép các mã ghế thành chuỗi
            string chuoiMaGhe = string.Join(", ", gheDat.Select(g => g.MaGhe));

            var doUongs = await _context.BookDoUong
                .Where(doUong => doUong.MaBook == maBook)
                .ToListAsync();
            List<DoUongDTO> listDrinks = new List<DoUongDTO>();
            foreach (var doUong in doUongs)
            {
                var drink = await _context.DoUong
                .FirstOrDefaultAsync(p => p.MaDoUong == doUong.MaDoUong);
                var doUongDTO = drink.Adapt<DoUongDTO>();
                doUongDTO.SoLuongDat = doUong.SoLuong;
                listDrinks.Add(doUongDTO);
            }
            return new DatVeThanhCongDTO
            {
                Phim = phim,
                SuatChieu = suatChieu,
                BookVe = bookVe,
                KhachHang = khachHang,
                GheDat = chuoiMaGhe,
                Drinks = listDrinks
            };
        }

    }
}
