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
            var phim = await _phimService.GetThongTinPhimAsync(maPhim);
            var listNgay = await _phimService.GetNgayXem(maPhim);  // Giả sử đây là List<string> chứa ngày tháng dưới dạng dd/MM/yyyy
            var listCaChieu = new List<ShiftModel>();
            var suatChieuTheoNgay = new Dictionary<string, List<ShiftModel>>();

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
                NgayChieu = listNgay.Select(ngay => 
        DateTime.TryParseExact(ngay, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime date) 
        ? date.ToString("dd-MM-yyyy") 
        : ngay).ToList(),
                SuatChieuTheoNgay = suatChieuTheoNgay
            };

            return View(model);
        }


    }
}
