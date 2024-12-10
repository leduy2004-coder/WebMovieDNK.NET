using Web.Api;
using WEB.Models;

namespace WEB.Api
{
    public class Admin_QLKhachHangService
    {
        private readonly ApiService _apiService;
        public Admin_QLKhachHangService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<Admin_QLKhachHangModel>> GetListKhachHang()
        {
            return await _apiService.GetDataAsync<List<Admin_QLKhachHangModel>>("/api/khachhang");

        }
        public async Task<Admin_QLKhachHangModel> LuuKhachHangListAsync(Admin_QLKhachHangModel md)
        {
            md.MaKH = null;
            return await _apiService.PostDataAsync<Admin_QLKhachHangModel>("api/khachhang", md);
        }

        public async Task<Admin_QLKhachHangModel> UpdateKhachHangAsync(Admin_QLKhachHangModel md)
        {
            string url = $"api/khachhang/{md.MaKH}";
            return await _apiService.PutDataAsync<Admin_QLKhachHangModel>(url, md);
        }
        public async Task<Admin_QLKhachHangModel> GetKhachHangAsync(string manv)
        {
            return await _apiService.GetDataAsync<Admin_QLKhachHangModel>("api/khachhang/khachhang");
        }

        public async Task<bool> DeleteKhachHangAsync(string maNV)
        {
            if (string.IsNullOrEmpty(maNV))
            {
                throw new ArgumentException("Mã khách hàng không hợp lệ.");
            }
            // Xây dựng URL API
            string url = $"api/khachhang/{maNV}";     
            bool deleteSuccess = await _apiService.DeleteDataAsync(url);

            return deleteSuccess;
        }

        public async Task<List<Admin_QLKhachHangModel>> SearchKHListAsync(string name)
        {
            string url = $"api/khachhang/tim/{name}"; ;

            return await _apiService.GetDataAsync<List<Admin_QLKhachHangModel>>(url);
        }
    }
}
