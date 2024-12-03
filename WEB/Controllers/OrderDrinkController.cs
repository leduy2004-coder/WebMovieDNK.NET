using Microsoft.AspNetCore.Mvc;

namespace WEB.Controllers
{
    public class OrderDrinkController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
