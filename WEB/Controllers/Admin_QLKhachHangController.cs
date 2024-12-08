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
    [HttpGet]
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        var listSC = await khService.GetListKhachHang();

        return View("Index", listSC.ToList());
    }

    [HttpPost]
    [Route("LuuKhachHang")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LuuKhacHang(Admin_QLKhachHangModel kh)
    {
        try
        {
            if (kh.MaKH != null)
            {
                await khService.UpdateKhachHangAsync(kh);

                // Chuyển hướng đến trang Index với thông báo thành công
                return RedirectToAction("Index", "Admin_QLKhachHang", new { actionType = "update", SaveSuccess = true });
            }
            else
            {
                var sanPham = khService.LuuKhachHangListAsync(kh);
                // Chuyển hướng đến trang Index với thông báo thành công
                return RedirectToAction("Index", "Admin_QLKhachHang", new { actionType = "create", SaveSuccess = true });
            }
        }
        catch (Exception ex)
        {
            return RedirectToAction("Index", "Admin_QLKhachHang", new { actionType = "create", SaveSuccess = false });
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

