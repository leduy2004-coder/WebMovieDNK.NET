using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WEB.Api;
using WEB.Models;

namespace WEB.Controllers
{
    public class Admin_QLSuatChieuController : Controller
    {
        private readonly Admin_SuatChieuService scService;
        public Admin_QLSuatChieuController(Admin_SuatChieuService scService)
        {
            this.scService = scService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var listSC = await scService.GetAllSuatChieu();

            return PartialView("Index", listSC.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LuuSuatChieu(Admin_SuatChieuModel sp)
        {

            var existingNhanVien = await scService.GetListSuatChieu(sp.MaSuat);


            if (existingNhanVien != null)
            {
                var NhanVien = scService.UpdateSuatChieuAsync(sp);

                // Chuyển hướng đến trang Index với thông báo thành công
                return RedirectToAction("", new { actionType = "update", SaveProductSuccess = true });
            }
            else
            {
                var sanPham = scService.LuuSuatChieuListAsync(sp);
                // Chuyển hướng đến trang Index với thông báo thành công
                return RedirectToAction("", new { actionType = "create", SaveProductSuccess = true });
            }
        }
    }
}
