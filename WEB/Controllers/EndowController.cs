using Microsoft.AspNetCore.Mvc;
using Web.Api;
using Web.Models;
using WEB.Api;
using WEB.Models;

namespace WEB.Controllers
{
    public class EndowController : Controller
    {
        private readonly EndowService _endowService;
        public EndowController(EndowService endowService)
        {
            this._endowService = endowService;
        }

        public async Task<IActionResult> Index()
        {
            var listPhimDC = await _endowService.GetUuDais();
       
            return View("Endow", listPhimDC);
        }
        public async Task<IActionResult> Detail(string maUuDai)
        {
            var detail = await _endowService.GetUuDaiById(maUuDai);
            
            return View("DetailEndow", detail);
        }

    }
}
