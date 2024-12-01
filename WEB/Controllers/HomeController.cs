using Microsoft.AspNetCore.Mvc;
using Web.Api;
using Web.Models;
using WEB.Models;


namespace Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly MovieService phimService;
        public HomeController(MovieService phimService)
        {
            this.phimService = phimService;
        }
        public async Task<IActionResult> Index()
        {
            var listPhimDC = await phimService.GetPhimDangChieu();
            var listPhimSC = await phimService.GetPhimSapChieu();

            var model = new HomeViewModel
            {
                PhimDangChieu = listPhimDC.ToList(),  
                PhimSapChieu = listPhimSC.ToList(),   
                DanhSachNgay = new List<DateTime>(),  
                CaChieu = new List<CaChieuModel>()  
            };

            return PartialView("Index", model);
        }

   

    }
}
