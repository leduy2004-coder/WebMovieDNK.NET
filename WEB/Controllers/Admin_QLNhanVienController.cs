using Microsoft.AspNetCore.Mvc;
using WEB.Api;
using WEB.Models;

[Route("Admin_QLNhanVien")]
public class Admin_QLNhanVienController : Controller
{
    private readonly NhanVienService nvService;
    public Admin_QLNhanVienController(NhanVienService nvService)
    {
        this.nvService = nvService;
    }

    // Phương thức này sẽ đáp ứng yêu cầu GET cho "Admin_QLNhanVien/Index"
    [HttpGet]
    [Route("Index")]
    public async Task<IActionResult> IndexAsync()
    {
        var listNV = await nvService.GetNhanVienListAsync();
        return PartialView("Index", listNV.ToList());
    }

    // Phương thức này sẽ đáp ứng yêu cầu POST cho "Admin_QLNhanVien/LuuNhanVien"
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LuuNhanVien(Admin_NhanVienModel sp)
    {

        var existingSanPham = await nvService.UpdateNhanVienAsync(sp);

        if (existingSanPham != null)
        {
            var sanPham = nvService.UpdateNhanVienAsync(sp);

            // Chuyển hướng đến trang Index với thông báo thành công
            return RedirectToAction("", new { actionType = "update", SaveProductSuccess = true });
        }
        else
        {
            var sanPham = nvService.LuuNhanVienListAsync(sp);
            // Chuyển hướng đến trang Index với thông báo thành công
            return RedirectToAction("", new { actionType = "create", SaveProductSuccess = true });
        }
    }

    // Phương thức này sẽ đáp ứng yêu cầu POST cho "Admin_QLNhanVien/DeleteNhanVien"
    [HttpPost]
    [Route("DeleteNhanVien")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteNhanVien(string maNV)
    {
        if (string.IsNullOrEmpty(maNV))
        {
            return RedirectToAction("Index", "Admin_QLNhanVien", new { actionType = "delete", SaveSuccess = false });
        }

        bool deleteSuccess = await nvService.DeleteNhanVienAsync(maNV);
        if (deleteSuccess)
        {
            return RedirectToAction("Index", "Admin_QLNhanVien", new { actionType = "delete", SaveSuccess = true });
        }

        return RedirectToAction("Index", "Admin_QLNhanVien", new { actionType = "delete", SaveSuccess = false });
    }
}
