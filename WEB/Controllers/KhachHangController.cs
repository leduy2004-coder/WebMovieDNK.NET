using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;

namespace WEB.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly KhachHangService _khachHangService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public KhachHangController(KhachHangService khachHangService, IHttpContextAccessor httpContextAccessor)
        {
            this._khachHangService = khachHangService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<IActionResult> Index()
        {
            var httpContext = _httpContextAccessor.HttpContext;
            var userId = httpContext.Session.GetString("UserId");
            KhachHangModel khachHang = new KhachHangModel();

            khachHang = await _khachHangService.GetKhachHangByIdAsync(userId);

            var lichSu = (await _khachHangService.GetLichSuKHAsync(userId)).ToList();

            ViewBag.KhachHang = khachHang;
            ViewBag.LichSu = lichSu;

            return View();
        }
    }
}
