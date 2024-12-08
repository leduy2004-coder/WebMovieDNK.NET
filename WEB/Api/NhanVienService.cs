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

            md.MaNV = null; // Đặt MaNV là null trước khi gửi dữ liệu
            return await _apiService.PostDataAsync<Admin_NhanVienModel>("api/nhanvien", md);

        }


        public async Task<Admin_NhanVienModel> UpdateNhanVienAsync(Admin_NhanVienModel md)
        {
            string url = $"api/nhanvien/{md.MaNV}";
            return await _apiService.PutDataAsync<Admin_NhanVienModel>(url, md);
        }
        public async Task<bool> DeleteNhanVienAsync(string maNV)
        {
            if (string.IsNullOrEmpty(maNV))
            {
                throw new ArgumentException("Mã nhân viên không hợp lệ.");
            }

            // Xây dựng URL API
            string url = $"api/nhanvien/{maNV}";

            // Gọi API để xóa nhân viên
            bool deleteSuccess = await _apiService.DeleteDataAsync(url);

            return deleteSuccess;
        }


    }
}
