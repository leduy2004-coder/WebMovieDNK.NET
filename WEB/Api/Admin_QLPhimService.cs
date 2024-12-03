using Web.Api;
using WEB.Models;

namespace WEB.Api
{
    public class Admin_QLPhimService
    {
        private readonly ApiService _apiService;
        public Admin_QLPhimService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<Admin_QLPhimModel>> GetListPhim()
        {
            return await _apiService.GetDataAsync<List<Admin_QLPhimModel>>("/api/phim/all");

        }
        public async Task<Admin_QLPhimModel> LuuPhimListAsync(Admin_QLPhimModel md)
        {
            return await _apiService.PostDataAsync<Admin_QLPhimModel>("api/phim", md);
        }

        public async Task<Admin_QLPhimModel> UpdatePhimAsync(Admin_QLPhimModel md)
        {
            string url = $"api/phim/{md.MaPhim}";
            return await _apiService.PutDataAsync<Admin_QLPhimModel>(url, md);
        }
        public async Task<Admin_QLPhimModel> GetPhimAsync(string manv)
        {
            return await _apiService.GetDataAsync<Admin_QLPhimModel>("api/phim/phim");
        }

        public async Task<bool> DeletePhimAsync(string maNV)
        {
            if (string.IsNullOrEmpty(maNV))
            {
                throw new ArgumentException("Mã khách hàng không hợp lệ.");
            }
            // Xây dựng URL API
            string url = $"api/phim/{maNV}";
            bool deleteSuccess = await _apiService.DeleteDataAsync(url);

            return deleteSuccess;
        }
    }

}
