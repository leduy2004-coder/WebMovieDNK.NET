using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    [Route("Admin_QLHome")]
    public class Admin_HomeController : Controller
    {
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
