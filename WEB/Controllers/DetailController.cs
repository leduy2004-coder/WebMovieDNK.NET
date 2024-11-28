using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGK.Api;

namespace WebGK.Controllers
{
    public class DetailController : Controller
    {
        private readonly MenuService sanPhamService;

        public DetailController(MenuService sanPhamService)
        {
            this.sanPhamService = sanPhamService;
        }
        public IActionResult Details(int id)
        {
            var menuItem = sanPhamService.GetSanPhamAsync(id);
            if (menuItem == null)
            {
                return NotFound();
            }
            return View(menuItem);
        }
    }
}
