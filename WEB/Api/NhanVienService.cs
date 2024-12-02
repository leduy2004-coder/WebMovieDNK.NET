using Web.Api;
using WEB.Models;


namespace WEB.Api
{
    public class NhanVienService
    {
        private readonly ApiService _apiService;
        public NhanVienService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<Admin_NhanVienModel>> GetNhanVienListAsync()
        {
            return await _apiService.GetDataAsync<List<Admin_NhanVienModel>>("api/nhanvien");
        }
        public async Task<Admin_NhanVienModel> GetNhanVienAsync(string manv)
        {
            return await _apiService.GetDataAsync<Admin_NhanVienModel>("api/nhanvien/nhanvien");
        }
        public async Task<Admin_NhanVienModel> LuuNhanVienListAsync(Admin_NhanVienModel md)
        {
            return await _apiService.PostDataAsync<Admin_NhanVienModel>("api/nhanvien", md);
        }

        public async Task<Admin_NhanVienModel> UpdateNhanVienAsync(Admin_NhanVienModel md)
        {
            string url = $"api/nhanvien/{md.MaNV}";
            return await _apiService.PutDataAsync<Admin_NhanVienModel>(url, md);
        }
    }
}
