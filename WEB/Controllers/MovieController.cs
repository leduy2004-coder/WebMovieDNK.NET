using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using Web.Api;
using Web.Models;
using WEB.Models;

namespace WEB.Controllers
{
    public class MovieController : Controller
    {
        private readonly MovieService _phimService;
        public MovieController(MovieService phimService)
        {
            this._phimService = phimService;
        }



        public async Task<IActionResult> Index(string maPhim)
        {
            // Lấy thông tin phim
            var phim = await _phimService.GetThongTinPhimAsync(maPhim);

            // Lấy danh sách ngày xem
            var listNgay = await _phimService.GetNgayXem(maPhim); // Giả sử đây là List<string> chứa ngày dưới dạng dd/MM/yyyy

            // Khởi tạo danh sách cần thiết
            var listCaChieu = new List<ShiftModel>();
            var suatChieuTheoNgay = new Dictionary<string, List<ShiftModel>>();
            var listMaSuat = new List<string>();
            foreach (var ngay in listNgay)
            {
                DateTime date;
                if (DateTime.TryParseExact(ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
                {
                    string ngayChieu = date.ToString("dd-MM-yyyy");

                    var caChieu = await _phimService.GetSuatChieu(maPhim, ngayChieu);
                    if (!suatChieuTheoNgay.ContainsKey(ngayChieu))
                    {
                        suatChieuTheoNgay[ngayChieu] = new List<ShiftModel>();
                    }

                    listMaSuat.AddRange(caChieu.Select(c => c.MaSuat));
                    suatChieuTheoNgay[ngayChieu].AddRange(caChieu);
                }
                else
                {
                    Console.WriteLine($"Lỗi khi chuyển đổi ngày: {ngay}");
                }
            }

            // Tạo ViewModel
            var model = new PhimViewModel
            {
                Phim = phim,
                MaSuat = listMaSuat,
                NgayChieu = listNgay.Select(ngay =>
                    DateTime.TryParseExact(ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date)
                    ? date.ToString("dd-MM-yyyy")
                    : ngay).ToList(),
                SuatChieuTheoNgay = suatChieuTheoNgay
            };

            return View(model);
        }


        public async Task<IActionResult> Showing()
        {
            var listPhim = await _phimService.GetPhimDangChieu();
            return View("Menu", listPhim);
        }

        public async Task<IActionResult> Coming()
        {
            var listPhim = await _phimService.GetPhimSapChieu();
            return View("Menu", listPhim);
        }

    }
}
