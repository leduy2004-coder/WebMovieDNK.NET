using Microsoft.AspNetCore.Mvc;
using Web.Api;
using Web.Models;
using WEB.Api;
using WEB.Models;


namespace Web.Controllers
{
    public class HomeController : Controller
    {

        private readonly MovieService phimService;
        private readonly EndowService _endowService;

        public HomeController(MovieService phimService, EndowService endowService)
        {
            this.phimService = phimService;
            this._endowService = endowService;
        }
        public async Task<IActionResult> Index()
        {
            var listPhimDC = await phimService.GetPhimDangChieu();
            var listPhimSC = await phimService.GetPhimSapChieu();
            var listUuDai = await _endowService.GetUuDais();
            var model = new HomeViewModel
            {
                PhimDangChieu = listPhimDC.ToList(),
                PhimSapChieu = listPhimSC.ToList(),
                DanhSachNgay = new List<DateTime>(),
                CaChieu = new List<ShiftModel>(),
                UuDai = listUuDai.ToList(), 
            };

            return PartialView("Index", model);
        }

   

    }
}
