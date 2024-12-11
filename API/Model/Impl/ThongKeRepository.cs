using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using API.Data;
using API.Dto;
using API.Model;
using Microsoft.EntityFrameworkCore;

namespace API.Model.Impl
{
    public class ThongKeRepository : IThongKeRepository
    {
        private readonly AppDbContext _context;

        public ThongKeRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task<int> GetSoLuongPhimDaChieuTrongNam(string nam)
        {
            // Truy vấn sử dụng FromSqlRaw để gọi hàm fsoLuongPhimDaChieuTrongNam từ SQL
            var result = await _context.Set<ThongKeVe>()
                .FromSqlRaw("SELECT dbo.fsoLuongPhimDaChieuTrongNam({0}) AS TongSoVe", nam)
                .FirstOrDefaultAsync();

            return result?.TongSoVe ?? 0;
        }
        public async Task<int> GetTongVeTrongNam(string nam)
        {
            // Truy vấn sử dụng FromSqlRaw để gọi hàm ftongVeTrongNam từ SQL
            var result = await _context.Set<ThongKeVe>()
           .FromSqlRaw("SELECT dbo.ftongVeTrongNam({0}) AS TongSoVe", nam)
           .FirstOrDefaultAsync();

            return result?.TongSoVe ?? 0;
        }

        public async Task<double> GetTongTienTheoNam(string nam)
        {
            // Truy vấn sử dụng FromSqlRaw để gọi hàm ftongDoanhThuTheoNam từ SQL
            var result = await _context.Set<DoanhThuTrongNamDTO>()
                .FromSqlRaw("SELECT dbo.ftongDoanhThuTheoNam({0}) AS TongTienTrongNam", nam)
                .FirstOrDefaultAsync();

            // Trả về giá trị kết quả (TongTienTrongNam), nếu không có giá trị trả về trả về 0
            return result?.TongTienTrongNam ?? 0;
        }

        public async Task<List<TopCustomerDTO>> GetTopCustomersByYear(string nam)
        {
            // Truy vấn sử dụng FromSqlRaw để gọi hàm GetTopCustomersByYear từ SQL
            var result = await _context.Set<TopCustomerDTO>()
                .FromSqlRaw("SELECT * FROM dbo.GetTopCustomersByYear({0})", nam)
                .ToListAsync();

            return result;
        }

        public async Task<List<int>> GetVeBanTungThang(string nam)
        {
            var results = await _context.Set<ThongKeVe>()
                .FromSqlRaw("SELECT Thang, TongSoVe FROM dbo.fThongKeTungThangTrongNam({0})", nam)
                .Select(dto => dto.TongSoVe)
                .ToListAsync();

            return results;
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
