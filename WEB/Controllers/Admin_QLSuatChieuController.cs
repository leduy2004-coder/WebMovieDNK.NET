﻿using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WEB.Api;
using WEB.Models;

namespace WEB.Controllers
{
    [Route("Admin_QLSuatChieu")]
    public class Admin_QLSuatChieuController : Controller
    {
        private readonly Admin_SuatChieuService scService;
        public Admin_QLSuatChieuController(Admin_SuatChieuService scService)
        {
            this.scService = scService;
        }
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var listSC = await scService.GetAllSuatChieu();

            return View("Index", listSC.ToList());
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteSuatChieu(string maNV)
        {
            if (string.IsNullOrEmpty(maNV))
            {
                return RedirectToAction("Index", "Admin_QLSuatChieu", new { actionType = "delete", SaveSuccess = false });
            }

            bool deleteSuccess = await scService.DeleteSuatChieuAsync(maNV);

            if (deleteSuccess)
            {
                return RedirectToAction("Index", "Admin_QLSuatChieu", new { actionType = "delete", SaveSuccess = true });
            }

            return RedirectToAction("Index", "Admin_QLSuatChieu", new { actionType = "delete", SaveSuccess = false });
        }
    }
}
