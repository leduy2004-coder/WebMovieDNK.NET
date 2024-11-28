using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGK.Api;
using WebGK.Models;
namespace WebGK.Controllers
{
    public class HomeController : Controller
    {


        private readonly MenuService sanPhamService;
        private readonly DanhMucService danhMucService;
        public HomeController(MenuService sanPhamService, DanhMucService danhMucService)
        {
            this.sanPhamService = sanPhamService;
            this.danhMucService = danhMucService;
        }
        public async Task<IActionResult> Index()
        {
            var listDanhMuc = await danhMucService.GetDanhMucListAsync();

            return PartialView("Index", listDanhMuc);
        }

    }
}
