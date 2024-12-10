using API.Data;
using API.Dto;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace API.Model
{
    public class SuatChieuRepository : ISuatChieuRepository
    {
        private readonly AppDbContext _context; // Khai báo DbContext để truy cập cơ sở dữ liệu

        // Constructor nhận vào ApplicationDbContext thông qua Dependency Injection
        public SuatChieuRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<tbSuatChieu>> GetSuatChuaChieu(string maPhim)
        {
            return await _context.SuatChieu
                .Where(sc => sc.TinhTrang == false && sc.MaPhim == maPhim) // Điều kiện lọc: Suất chiếu chưa chiếu và mã phim
                .ToListAsync(); // Thực hiện truy vấn và trả về danh sách suất chiếu chưa chiếu
        }

        public async Task<IEnumerable<DateTime>> GetNgayChieuTheoPhim(string maPhim)
        {
            var ngayChieuList = await _context.Set<GetNgayChieu>()
                .FromSqlRaw("SELECT * FROM dbo.fXuatNgayChieu({0})", maPhim)
                .ToListAsync();

            return ngayChieuList.Select(n => n.NgayChieu);
        }

        public Task<IEnumerable<tbSuatChieu>> GetSuatChieuTheoPhim(string maPhim)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<GetCaChieu>> GetCaChieuTheoPhimVaNgay(string maPhim, string ngayChieu)
        {
            try
            {
                // Chuyển đổi định dạng ngày tháng từ chuỗi
                DateTime parsedDate = DateTime.ParseExact(ngayChieu, "dd-MM-yyyy", System.Globalization.CultureInfo.InvariantCulture);

                // Tạo các tham số SqlParameter
                var maPhimParam = new SqlParameter("@maPhim", maPhim);
                var ngayChieuParam = new SqlParameter("@ngayChieu", parsedDate);

                // Gọi SQL function với tham số truyền vào
                var caChieuList = await _context.Set<GetCaChieu>()
                    .FromSqlRaw("SELECT * FROM dbo.fXuatThoiGianChieu(@maPhim, @ngayChieu)", maPhimParam, ngayChieuParam)
                    .ToListAsync();

                return caChieuList;
            }
            catch (FormatException ex)
            {
                // Xử lý lỗi nếu định dạng ngày không hợp lệ
                Console.WriteLine($"Lỗi định dạng ngày: {ex.Message}");
                throw;
            }
        }

        public async Task<bool> DeleteSuatChieu(string maSuatChieu)
        {
            var suatChieu = await _context.SuatChieu.FindAsync(maSuatChieu);
            if (suatChieu == null) return false;

            suatChieu.TinhTrang = false;
            _context.SuatChieu.Update(suatChieu);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<tbSuatChieu> addSuatChieu(SuatChieuDTO suatChieuDTO)
        {
            // Chuyển đổi DTO sang Entity
            var suatChieu = new tbSuatChieu
            {
                MaSuat = "",
                MaPhim = suatChieuDTO.MaPhim,
                MaPhong = suatChieuDTO.MaPhong,
                MaCa = suatChieuDTO.MaCa,
                NgayChieu = suatChieuDTO.NgayChieu,
                TinhTrang = suatChieuDTO.TinhTrang
            };

            // Thêm vào cơ sở dữ liệu
            _context.SuatChieu.Add(suatChieu);
            await _context.SaveChangesAsync();

            // Trả về suất chiếu đã thêm
            return suatChieu;
        }

        public async Task<tbSuatChieu> upadteSuatChieu(SuatChieuDTO suatChieu)
        {
            // Tìm suất chiếu theo mã
            var existingSuatChieu = await _context.SuatChieu.FindAsync(suatChieu.MaSuat);
            if (existingSuatChieu == null) return null; // Nếu không tìm thấy, trả về null

            // Cập nhật các thuộc tính của suất chiếu
            existingSuatChieu.MaPhim = suatChieu.MaPhim;
            existingSuatChieu.MaPhong = suatChieu.MaPhong;
            existingSuatChieu.MaCa = suatChieu.MaCa;
            existingSuatChieu.NgayChieu = suatChieu.NgayChieu;
            existingSuatChieu.TinhTrang = suatChieu.TinhTrang;

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();

            // Trả về suất chiếu đã cập nhật
            return existingSuatChieu;
        }

        public async Task<tbSuatChieu> GetSuatChieuTheoMaSC(string maSC)
        {
            return await _context.SuatChieu
                .Where(SC => SC.MaSuat == maSC)
                .FirstOrDefaultAsync();  
        }

        public async Task<List<tbSuatChieu>> GetAllSuatChieu()
        {
            return await _context.SuatChieu.ToListAsync();
        }


        public async Task<IEnumerable<tbGhe>> GetGheDaDat(string maSC)
        {
            var maSuat = new SqlParameter("@maSuat", maSC);

            // Gọi SQL function với tham số truyền vào
            var gheList = await _context.Set<tbGhe>()
                .FromSqlRaw("SELECT * FROM dbo.fSoGheDaDat(@maSuat)", maSuat)
                .ToListAsync();

            return gheList;
        }

        public async Task<tbCaChieu> GetCaChieuTheoMaCa(string maCa)
        {
            return await _context.CaChieu
                .Where(SC => SC.MaCa == maCa)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<tbGhe>> GetAllGhe()
        {
            return await _context.Ghe.ToListAsync();
        }

   

        public async Task<IEnumerable<tbCaChieu>> GetAvailableCaChieuForSuatChieu(string maPhim, DateTime ngayChieu)
        {
            var availableCaChieu = await _context.CaChieu
              .Where(ca => !_context.SuatChieu
                  .Any(sc => sc.MaCa == ca.MaCa && sc.NgayChieu == ngayChieu && sc.MaPhim == maPhim))
              .ToListAsync();

            return availableCaChieu;
        }

        public async Task<IEnumerable<tbPhongChieu>> GetAvailablePhongChieuForSuatChieu(string maCa, DateTime ngayChieu)
        {
            var availablePhongChieu = await _context.PhongChieu
            .Where(phong => !_context.SuatChieu
                .Any(sc => sc.MaPhong == phong.MaPhong && sc.MaCa == maCa && sc.NgayChieu == ngayChieu))
            .ToListAsync();

            return availablePhongChieu;
        }
    }
}
