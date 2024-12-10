using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    [Route("Admin_QLHome")]
    public class Admin_HomeController : Controller
    {
        public IActionResult Index()
        {
            var data = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 11 };
            return View(data);
        }
    }
}
