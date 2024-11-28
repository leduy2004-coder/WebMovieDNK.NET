//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using WebGK.Api;
//using WebGK.Models;

//namespace WebGK.Controllers
//{
//    public class MenuController : Controller
//    {
//        private readonly MenuService sanPhamService;
//        private readonly DanhMucService danhMucService;
//        public MenuController(MenuService sanPhamService, DanhMucService danhMucService)
//        {
//            this.sanPhamService = sanPhamService;
//            this.danhMucService = danhMucService;
//        }


//        [HttpGet]
//        public async Task<IActionResult> GetMenuByCategory(int danhmucid)
//        {
//            var dishes = await sanPhamService.GetSanPhamByDanhMucAsync(danhmucid);
//            return PartialView("GetMenuByCategory", dishes);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> LuuSanPham(SanPhamViewModel sp)
//        {

//            var existingSanPham = await sanPhamService.GetSanPhamAsync(sp.SanPhamMoi.MaSanPhamID);

//            if (existingSanPham != null)
//            {
//                var sanPham = sanPhamService.PutSanPhamAsync(sp.SanPhamMoi);

//                // Chuyển hướng đến trang Index với thông báo thành công
//                return RedirectToAction("", new { actionType = "update", SaveProductSuccess = true });
//            }
//            else
//            {
//                var sanPham = sanPhamService.PostSanPhamAsync(sp.SanPhamMoi);
//                // Chuyển hướng đến trang Index với thông báo thành công
//                return RedirectToAction("", new { actionType = "create", SaveProductSuccess = true });
//            }
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        public ActionResult XoaSanPham(int maSanPhamXoa)
//        {

//            var existingSanPham = sanPhamService.GetSanPhamAsync(maSanPhamXoa);

//            if (existingSanPham.Result != null)
//            {
//                var check = sanPhamService.DeleteSanPhamAsync(maSanPhamXoa);
//            }

//            return RedirectToAction("", new { actionType = "delete", SaveProductSuccess = true });
//        }

//        [HttpGet]
//        public async Task<IActionResult> TimSanPham(string searchTerm)
//        {
//            var listSanPham = await sanPhamService.SearchSanPhamListAsync(searchTerm);
//            var listDanhMuc = await danhMucService.GetDanhMucListAsync();
//            var viewModel = new SanPhamViewModel
//            {
//                DanhSachSanPham = listSanPham,
//                SanPhamMoi = new SanPham(),
//                DanhSachDM = listDanhMuc
//            };
//            return PartialView("Product", viewModel);
//        }
//    }
//}
