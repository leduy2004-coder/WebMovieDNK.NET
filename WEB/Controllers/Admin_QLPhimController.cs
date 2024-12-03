using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;

namespace WEB.Controllers
{
    public class Admin_QLPhimController : Controller
    {
        private readonly Admin_QLPhimService khService;
        public Admin_QLPhimController(Admin_QLPhimService khService)
        {
            this.khService = khService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var listSC = await khService.GetListPhim();

            return PartialView("Index", listSC.ToList());
        }

        [HttpPost]
        [Route("LuuPhim")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LuuPhim(Admin_QLPhimModel sp)
        {

            if (sp.MaPhim != null)
            {
                var NhanVien = khService.UpdatePhimAsync(sp);

                // Chuyển hướng đến trang Index với thông báo thành công
                return RedirectToAction("Index", "Admin_QLPhim", new { actionType = "update", SaveSuccess = true });
            }
            else
            {
                var sanPham = khService.LuuPhimListAsync(sp);
                // Chuyển hướng đến trang Index với thông báo thành công
                return RedirectToAction("Index", "Admin_QLPhim", new { actionType = "create", SaveSuccess = true });
            }
        }
        [HttpPost]
        [Route("DeletePhim")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeletePhim(string maNV)
        {
            if (string.IsNullOrEmpty(maNV))
            {
                return RedirectToAction("Index", "Admin_QLPhim", new { actionType = "delete", SaveSuccess = false });
            }

            bool deleteSuccess = await khService.DeletePhimAsync(maNV);

            if (deleteSuccess)
            {
                return RedirectToAction("Index", "Admin_QLPhim", new { actionType = "delete", SaveSuccess = true });
            }

            return RedirectToAction("Index", "Admin_QLPhim", new { actionType = "delete", SaveSuccess = false });
        }
    }
}
