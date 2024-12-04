using API.Data;
using KTGiuaKi.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Model
{

    public class DatDoUongRepository : IDatDoUongRepository
    {
        private readonly AppDbContext _context;

        public DatDoUongRepository(AppDbContext context)
        {
            _context = context;
        }


        // Thêm mới BookDoUong
        public async Task<tbBookDoUong> DatDoUong(tbBookDoUong tbBook)
        {
            await _context.BookDoUong.AddAsync(tbBook);
            await _context.SaveChangesAsync();
            return tbBook;
        }

        public async Task<List<tbDoUong>> LayThongTinDoUong()
        {
            return await _context.DoUong.ToListAsync();
        }

    }
}
