using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Web.Api;
using Web.Models;
using WEB.Api;
using WEB.Models;

namespace WEB.Controllers
{
    [Route("Admin_QLSuatChieu")]
    public class Admin_QLSuatChieuController : Controller
    {
        private readonly Admin_SuatChieuService scService;
        private readonly MovieService _movieService;

        public Admin_QLSuatChieuController(Admin_SuatChieuService scService, MovieService movieService)
        {
            this.scService = scService;
            this._movieService = movieService;
        }
        [HttpGet]

        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var listSC = await scService.GetAllSuatChieu();
            var listPhim = await _movieService.GetPhimDangChieu();

            var model = new Admin_SuatChieuViewModel
            {
                listSuatChieu = listSC.ToList(),
                PhimDangChieu = listPhim.ToList(),
                DanhSachNgay = new List<DateTime>(),
                CaChieu = new List<ShiftModel>(),
                PhongChieu = new List<PhongChieuModel>(),

            };
            return View("Index", model);
        }

        [HttpPost]
        [Route("LuuSuatChieu")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LuuSuatChieu(Admin_SuatChieuModel sc)
        {

            try
            {
                if (sc.MaSuat != null)
                {
                    var suatChieu = scService.UpdateSuatChieuAsync(sc);
                    return RedirectToAction("Index", "Admin_QLSuatChieu", new { actionType = "update", SaveSuccess = true });

                }
                else
                {
                    var sanPham = scService.LuuSuatChieuListAsync(sc);
                    // Chuyển hướng đến trang Index với thông báo thành công
                    return RedirectToAction("Index", "Admin_QLSuatChieu", new { actionType = "create", SaveSuccess = true });
                }
               
                
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Admin_QLSuatChieu", new { actionType = "create", SaveSuccess = false });
            }
        }

        [HttpPost("DeleteSuatChieu")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSuatChieu(string maSuatChieu)
        {
            if (string.IsNullOrEmpty(maSuatChieu))
            {
                return RedirectToAction("Index", "Admin_QLSuatChieu", new { actionType = "delete", SaveSuccess = false });
            }

            bool deleteSuccess = await scService.DeleteSuatChieuAsync(maSuatChieu);

            if (deleteSuccess)
            {
                return RedirectToAction("Index", "Admin_QLSuatChieu", new { actionType = "delete", SaveSuccess = true });
            }

            return RedirectToAction("Index", "Admin_QLSuatChieu", new { actionType = "delete", SaveSuccess = false });
        }

    }
}
