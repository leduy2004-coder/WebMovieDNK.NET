using Microsoft.AspNetCore.Mvc;
using Web.Api;


namespace Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly PhimService phimService;
        public HomeController(PhimService phimService)
        {
            this.phimService = phimService;
        }
        public async Task<IActionResult> Index()
        {
            var listPhim = await phimService.GetPhimListAsync();

            return PartialView("Index", listPhim);
        }

    }
}
