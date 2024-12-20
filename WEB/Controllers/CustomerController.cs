﻿using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;

namespace WEB.Controllers
{
    public class CustomerController : Controller
    {
        private readonly CustomerService _khachHangService;
        public CustomerController(CustomerService khachHangService)
        {
            this._khachHangService = khachHangService;
        }

        public async Task<IActionResult> Index()
        {
            var maKH = HttpContext.Session.GetString("UserId");
            CustomerModel khachHang = new CustomerModel();

            khachHang = await _khachHangService.GetKhachHangByIdAsync(maKH);

            var lichSu = (await _khachHangService.GetLichSuKHAsync(maKH)).ToList();

            ViewBag.KhachHang = khachHang;
            ViewBag.LichSu = lichSu;

            return View();
        }
    }
}
