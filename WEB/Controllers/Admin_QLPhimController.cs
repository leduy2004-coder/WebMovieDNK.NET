using Microsoft.AspNetCore.Mvc;
using Web.Models;
using WEB.Api;
using WEB.Models;

namespace WEB.Controllers
{
    [Route("Admin_QLPhim")]
    public class Admin_QLPhimController : Controller
    {
        private readonly Admin_QLPhimService khService;
        public Admin_QLPhimController(Admin_QLPhimService khService)
        {
            this.khService = khService;
        }
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var listMovie = await khService.GetListPhim();
            var listType = await khService.GetListTLPhim();
            var model = new Admin_ViewMovie
            {
                ListMovies = listMovie.ToList(),
                ListTypeMovie = listType,
            };
            return View("Index", model);
        }

        [HttpPost]
        [Route("LuuPhim")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LuuPhim(Admin_QLPhimModel md, IFormFile hinhDaiDienFile)
        {
            try
            {
                if (md.MaPhim != null)
                {
                    var NhanVien = await khService.SaveOrUpdatePhimAsync(md, hinhDaiDienFile, "api/phim", HttpMethod.Put);

                    return RedirectToAction("Index", "Admin_QLPhim", new { actionType = "update", SaveSuccess = true });
                }
                else
                {
                    var sanPham = await khService.SaveOrUpdatePhimAsync(md, hinhDaiDienFile, "api/phim", HttpMethod.Post);

                    return RedirectToAction("Index", "Admin_QLPhim", new { actionType = "create", SaveSuccess = true });
                }
            }
            catch (Exception ex)
            {
                return RedirectToAction("Index", "Admin_QLPhim", new { actionType = "create", SaveSuccess = false });
            }

        }
        [HttpPost]
        [Route("DeletePhim")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePhim(string maPhim)
        {
            if (string.IsNullOrEmpty(maPhim))
            {
                return RedirectToAction("Index", "Admin_QLPhim", new { actionType = "delete", SaveSuccess = false });
            }

            bool deleteSuccess = await khService.DeletePhimAsync(maPhim);

            if (deleteSuccess)
            {
                return RedirectToAction("Index", "Admin_QLPhim", new { actionType = "delete", SaveSuccess = true });
            }

            return RedirectToAction("Index", "Admin_QLPhim", new { actionType = "delete", SaveSuccess = false });
        }
    }
}
