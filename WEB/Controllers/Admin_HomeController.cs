using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;

namespace WEB.Controllers
{
    [Route("Admin_QLHome")]
    public class Admin_HomeController : Controller
    {
        private readonly Admin_ThongKeService service;
        public Admin_HomeController(Admin_ThongKeService service)
        {
            this.service = service;
        }
        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index()
        {
            var model = await service.GetThongKeAsync("2024");

            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> getThongKe(string selectedYear)
        {
            if (string.IsNullOrEmpty(selectedYear))
            {
                selectedYear = "2024"; 
            }
            ViewData["SelectedYear"] = selectedYear;

            var model = await service.GetThongKeAsync(selectedYear);

            return View("Index", model); 
        }
    }
}
