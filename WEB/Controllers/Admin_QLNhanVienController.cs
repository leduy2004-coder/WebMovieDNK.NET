using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WEB.Api;
using WEB.Models;

[Route("Admin_QLNhanVien")]
public class Admin_QLNhanVienController : Controller
{
    private readonly Admin_QLNhanVienService nvService;
    public Admin_QLNhanVienController(Admin_QLNhanVienService nvService)
    {
        this.nvService = nvService;
    }

    // Phương thức này sẽ đáp ứng yêu cầu GET cho "Admin_QLNhanVien/Index"
    [HttpGet]
    [Route("Index")]
    public async Task<IActionResult> Index()
    {
        var listNV = await nvService.GetNhanVienListAsync();
        return View("Index", listNV.ToList());
    }

    // Phương thức này sẽ đáp ứng yêu cầu POST cho "Admin_QLNhanVien/LuuNhanVien"
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> LuuNhanVien(Admin_NhanVienModel nv)
    {
        try
        {
            if (nv.MaNV != null)
            {
                var sanPham = await nvService.UpdateNhanVienAsync(nv);

                // Chuyển hướng đến trang Index với thông báo thành công
                return RedirectToAction("Index", "Admin_QLNhanVien", new { actionType = "update", SaveSuccess = true });
            }
            else
            {
                var nhanVien = await nvService.LuuNhanVienListAsync(nv);
              
                // Chuyển hướng đến trang Index với thông báo thành công
                return RedirectToAction("Index", "Admin_QLNhanVien", new { actionType = "create", SaveSuccess = true });
            }
        }
        catch (Exception ex)
        {
            return RedirectToAction("Index", "Admin_QLNhanVien", new { actionType = "create", SaveSuccess = false });
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

    [HttpGet]
    public async Task<IActionResult> TimNhanVien(string searchTerm)
    {
         var listEmploy = string.IsNullOrEmpty(searchTerm)
               ? await  nvService.GetNhanVienListAsync()
               : await nvService.SearchNVListAsync(searchTerm);

        return View("Index", listEmploy.ToList());
    }
}
