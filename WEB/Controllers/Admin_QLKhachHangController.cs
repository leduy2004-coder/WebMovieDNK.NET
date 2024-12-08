using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;

[Route("Admin_QLKhachHang")]


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

        [HttpPost]
    [Route("LuuKhachHang")]
    [ValidateAntiForgeryToken]
        public async Task<IActionResult> LuuKhacHang(Admin_QLKhachHangModel sp)
        {
        var existingSanPham = await khService.UpdateKhachHangAsync(sp);

        if (existingSanPham != null)
            {
                var kh = khService.UpdateKhachHangAsync(sp);

                // Chuyển hướng đến trang Index với thông báo thành công
                return RedirectToAction("Index", "Admin_QLKhachHang", new { actionType = "update", SaveSuccess = true });
            }
            else
            {
                var sanPham = khService.LuuKhachHangListAsync(sp);
                // Chuyển hướng đến trang Index với thông báo thành công
                return RedirectToAction("Index", "Admin_QLKhachHang", new { actionType = "create", SaveSuccess = true });
            }
        }
        [HttpPost]
    [Route("DeleteKhachHang")]
    [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteKhachHang(string maNV)
        {
            if (string.IsNullOrEmpty(maNV))
            {
                return RedirectToAction("Index", "Admin_QLKhachHang", new { actionType = "delete", SaveSuccess = false });
            }

            bool deleteSuccess = await khService.DeleteKhachHangAsync(maNV);

            if (deleteSuccess)
            {
                return RedirectToAction("Index", "Admin_QLKhachHang", new { actionType = "delete", SaveSuccess = true });
            }

            return RedirectToAction("Index", "Admin_QLKhachHang", new { actionType = "delete", SaveSuccess = false });
        }
    }

