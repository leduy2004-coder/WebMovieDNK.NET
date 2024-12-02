using Microsoft.AspNetCore.Mvc;
using Web.Api;
using WEB.Api;
using WEB.Models;


namespace WEB.Controllers
{
    public class Admin_QLNhanVienController : Controller
    {
        private readonly NhanVienService nvService;
        public Admin_QLNhanVienController(NhanVienService nvService)
        {
            this.nvService = nvService;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var listNV = await nvService.GetNhanVienListAsync();


            
            return PartialView("Index", listNV.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LuuNhanVien(Admin_NhanVienModel sp)
        {

            var existingNhanVien = await nvService.GetNhanVienAsync(sp.MaNV);


            if (existingNhanVien != null)
            {
                var NhanVien = nvService.UpdateNhanVienAsync(sp);

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
        //public async Task<IActionResult> Product()
        //{
        //    var listSanPham = await sanPhamService.GetSanPhamListAsync();
        //    var listDanhMuc = await danhMucService.GetDanhMucListAsync();
        //    var viewModel = new SanPhamViewModel
        //    {
        //        DanhSachSanPham = listSanPham,
        //        SanPhamMoi = new SanPham(),
        //        DanhSachDM = listDanhMuc
        //    };
        //    return PartialView("Product", viewModel);
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> LuuNhanVien(SanPhamViewModel sp)
        //{

        //    var existingSanPham = await sanPhamService.GetSanPhamAsync(sp.SanPhamMoi.MaSanPhamID);

        //    if (existingSanPham != null)
        //    {
        //        var sanPham = sanPhamService.PutSanPhamAsync(sp.SanPhamMoi);

        //        // Chuyển hướng đến trang Index với thông báo thành công
        //        return RedirectToAction("", new { actionType = "update", SaveProductSuccess = true });
        //    }
        //    else
        //    {
        //        var sanPham = sanPhamService.PostSanPhamAsync(sp.SanPhamMoi);
        //        // Chuyển hướng đến trang Index với thông báo thành công
        //        return RedirectToAction("", new { actionType = "create", SaveProductSuccess = true });
        //    }
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult XoaSanPham(int maSanPhamXoa)
        //{

        //    var existingSanPham = sanPhamService.GetSanPhamAsync(maSanPhamXoa);

        //    if (existingSanPham.Result != null)
        //    {
        //        var check = sanPhamService.DeleteSanPhamAsync(maSanPhamXoa);
        //    }

        //    return RedirectToAction("", new { actionType = "delete", SaveProductSuccess = true });
        //}

        //[HttpGet]
        //public async Task<IActionResult> TimSanPham(string searchTerm)
        //{
        //    var listSanPham = await sanPhamService.SearchSanPhamListAsync(searchTerm);
        //    var listDanhMuc = await danhMucService.GetDanhMucListAsync();
        //    var viewModel = new SanPhamViewModel
        //    {
        //        DanhSachSanPham = listSanPham,
        //        SanPhamMoi = new SanPham(),
        //        DanhSachDM = listDanhMuc
        //    };
        //    return PartialView("Product", viewModel);
        //}
    }
}
