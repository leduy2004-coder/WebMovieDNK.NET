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
    }
}
