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
        public async Task<IActionResult> Index(Admin_HomeView model)
        {

            return View("Index", model);
        }

        [HttpGet]
        public async Task<IActionResult> getThongKe(string nam)
        {
            var model = await service.GetThongKeAsync(nam);

            return View("Index", model);
        }
    }
}
