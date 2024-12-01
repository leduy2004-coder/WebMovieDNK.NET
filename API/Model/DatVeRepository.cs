using API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Model
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

    }
}
