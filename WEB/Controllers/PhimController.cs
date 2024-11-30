using Microsoft.AspNetCore.Mvc;
using Web.Api;

namespace WEB.Controllers
{
    public class PhimController : Controller
    {
        private readonly PhimService _phimService;
        public PhimController(PhimService phimService)
        {
            this._phimService = phimService;
        }

        [HttpGet("Phim/{maPhim}")]
        public async Task<IActionResult> Index(string maPhim)
        {
            var phim = await _phimService.GetThongTinPhimAsync(maPhim);
            return View(phim);
        }
    }
}
