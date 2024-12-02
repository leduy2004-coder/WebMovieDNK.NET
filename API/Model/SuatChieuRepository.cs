using API.Data;
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

        public async Task<bool> deleteSuatChieu(string maSuat)
        {
            var suatChieu = await _context.SuatChieu.FindAsync(maSuat);
            if (suatChieu == null)
            {
                return false;  // Nếu không tìm thấy suất chiếu, trả về false
            }

            _context.SuatChieu.Remove(suatChieu);  // Xóa suất chiếu khỏi cơ sở dữ liệu
            await _context.SaveChangesAsync();  // Lưu thay đổi vào cơ sở dữ liệu
            return true;  // Trả về true nếu xóa thành công
        }

        public Task<tbSuatChieu> addSuatChieu(string maSuat)
        {
            throw new NotImplementedException();
        }

        public Task<tbSuatChieu> upadteSuatChieu(string maSuat)
        {
            throw new NotImplementedException();
        }

        public async Task<tbSuatChieu> GetSuatChieuTheoMa(string maSC)
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
    }
}
