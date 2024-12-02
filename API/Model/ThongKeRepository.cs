using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using API.Data;
using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class ThongKeRepository : IThongKeRepository
    {
        private readonly AppDbContext _context;

        public ThongKeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<tbPhim>> ThongKe()
        {
            var result = await (from p in _context.Phim
                                where p.TinhTrang == true // Lọc các phim có TinhTrang = 1 (đang chiếu)
                                select new tbPhim
                                {
                                    MaPhim = p.MaPhim,
                                    TenPhim = p.TenPhim
                                }).ToListAsync();

            return result;
        }


        //public async Task<List<tbPhim>> ThongKe()
        //{
        //    var result = await (from p in _context.Phim
        //                        join v in _context.Ve on p.MaPhim equals v.MaPhim into pv
        //                        from v in pv.DefaultIfEmpty()
        //                        join b in _context.BookVe on v.MaVe equals b.MaVe into vb
        //                        from b in vb.DefaultIfEmpty()
        //                        where p.TinhTrang == true // Chỉ lấy phim đang chiếu
        //                        group b by new { p.MaPhim, p.TenPhim } into g
        //                        select new tbPhim
        //                        {
        //                            MaPhim = g.Key.MaPhim,
        //                            TenPhim = g.Key.TenPhim,
        //                            LuongVeDaBan = g.Sum(x => x == null ? 0 : x.SoLuongVeDaDat),
        //                            DoanhThu = g.Sum(x => x == null ? 0 : x.TongTien) ?? 0
        //                        })
        //                        .OrderByDescending(x => x.DoanhThu)
        //                        .ToListAsync();

        //    return result;
        //}
    }
}
