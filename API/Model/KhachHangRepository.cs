using API.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Model
{
    public class KhachHangRepository : IKhachHangRepository
    {
        private readonly AppDbContext _context;


        public KhachHangRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<tbKhachHang>> GetDanhSachKhachHang()
        {
            return await _context.KhachHang.ToListAsync();
        }
    }
}
