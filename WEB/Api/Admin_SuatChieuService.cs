using Web.Api;
using WEB.Models;

namespace WEB.Api
{
    public class Admin_SuatChieuService
    {
        private readonly ApiService _apiService;
        public Admin_SuatChieuService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<Admin_SuatChieuModel> GetSuatChieuListAsync()
        {
            return await _apiService.GetDataAsync<Admin_SuatChieuModel>("api/SuatChieu/GetSuatChuaChieu/{maPhim}");

        }

        public async Task<List<Admin_SuatChieuModel>> GetAllSuatChieu()
        {
            return await _apiService.GetDataAsync<List<Admin_SuatChieuModel>>("/api/Admin_QLSuatChieu");

        }

        public async Task<Admin_SuatChieuModel> GetListSuatChieu(string ms)
        {
            return await _apiService.GetDataAsync<Admin_SuatChieuModel>("/api/Admin_QLSuatChieu");

        }

        public async Task<Admin_NhanVienModel> LuuSuatChieuListAsync(Admin_SuatChieuModel md)
        {
            return await _apiService.PostDataAsync<Admin_NhanVienModel>("api/suatchieu", md);
        }

        public async Task<Admin_SuatChieuModel> UpdateSuatChieuAsync(Admin_SuatChieuModel md)
        {
            string url = $"api/suatchieu/{md.MaSuat}";
            return await _apiService.PutDataAsync<Admin_SuatChieuModel>(url, md);
        }
        public async Task<bool> DeleteSuatChieuAsync(string maNV)
        {
            if (string.IsNullOrEmpty(maNV))
            {
                throw new ArgumentException("Mã suất chiếu không hợp lệ.");
            }

            // Xây dựng URL API
            string url = $"api/suatchieu/{maNV}";

            // Gọi API để xóa nhân viên
            bool deleteSuccess = await _apiService.DeleteDataAsync(url);

            return deleteSuccess;
        }
    }
}
