using API.Data;
using Microsoft.EntityFrameworkCore;

namespace API.Model
{
    public class UuDaiRepository : IUuDaiRepository 
    {
        private readonly AppDbContext _context;

        public UuDaiRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<tbUuDai> GetUuDaiById(string maUuDai)
        {
            return await _context.UuDai.FindAsync(maUuDai);
        }

        public async Task<IEnumerable<tbUuDai>> GetUuDais()
        {
            try
            {
                return await _context.UuDai.ToListAsync();
            }
            catch (Exception ex)
            {
                // Ghi log lỗi
                throw new Exception("An error occurred while fetching data.");
            }
        }
    }
}
