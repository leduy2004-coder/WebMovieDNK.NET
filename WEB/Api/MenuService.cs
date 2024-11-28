//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using WebGK.Models;

//namespace WebGK.Api
//{
//    public class MenuService
//    {
//        private readonly ApiService _apiService;
//        public MenuService(ApiService apiService)
//        {
//            _apiService = apiService;
//        }
//        public async Task<List<Menu>> GetSanPhamListAsync()
//        {
//            return await _apiService.GetDataAsync<List<Menu>>("api/Menu");
//        }

//        public async Task<Menu> GetSanPhamAsync(int masanphamid)
//        {
//            string url = $"api/Menu/sanpham/{masanphamid}";
//            try
//            {
//                var sanPham = await _apiService.GetDataAsync<Menu>(url);
//                return sanPham;
//            }
//            catch (Exception ex)
//            {
//                return null; 
//            }
//        }

//        public async Task<Menu> GetSanPhamByDanhMucAsync(int danhmucid)
//        {
//            string url = $"api/Menu/danhmuc/{danhmucid}";
//            try
//            {
//                var sanPham = await _apiService.GetDataAsync<Menu>(url);
//                return sanPham;
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//        }
//        public async Task<SanPham> PostSanPhamAsync(SanPham sanPham)
//        {
//            try
//            {
//                return await _apiService.PostDataAsync<SanPham>("api/Product", sanPham);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"Lỗi khi thêm sản phẩm: {ex.Message}");
//                return null; // Hoặc bạn có thể ném lại ngoại lệ
//            }
//        }

//        public async Task<SanPham> PutSanPhamAsync(SanPham sanPham)
//        {
//            string url = $"api/Product/{sanPham.MaSanPhamID}";
//            return await _apiService.PutDataAsync<SanPham>(url, sanPham);
//        }

//        public async Task<bool> DeleteSanPhamAsync(int masanphamid)
//        {
//            string url = $"api/Product/{masanphamid}";
//            return await _apiService.DeleteDataAsync(url);
//        }

//        public async Task<List<SanPham>> SearchSanPhamListAsync(string name)
//        {
//            string url = string.IsNullOrEmpty(name) ? "api/Product/search" : $"api/Product/search?name={Uri.EscapeDataString(name)}";

//            return await _apiService.GetDataAsync<List<SanPham>>(url);
//        }

//    }


//}
