using Microsoft.AspNetCore.Mvc;
using WEB.Api;

namespace WEB.Controllers
{
    public class Admin_QLKhachHangController : Controller
    {
        private readonly Admin_QLKhachHangService khService;
        public Admin_QLKhachHangController(Admin_QLKhachHangService khService)
        {
            this.khService = khService;
        }
        public async Task<IActionResult> IndexAsync()
        {
            var listSC = await khService.GetListKhachHang();

            return PartialView("Index", listSC.ToList());
        }
    }
}
