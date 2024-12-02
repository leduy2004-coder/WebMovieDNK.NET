using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB.Models;
using Web.Api;

namespace WEB.Api
{
    public class Admin_ThongKeService
    {
        private readonly ApiService _apiService;

        // Constructor nhận ApiService qua Dependency Injection
        public Admin_ThongKeService(ApiService apiService)
        {
            _apiService = apiService;
        }

        // Phương thức gọi API để lấy dữ liệu thống kê
        public async Task<List<PhimModel>> GetThongKeAsync()
        {
            // Gửi yêu cầu GET đến API Thống kê
            var response = await _apiService.GetDataAsync<List<PhimModel>>("/api/thongke");

            // Kiểm tra phản hồi từ API
            if (response != null)
            {
                return response; // Trả về danh sách phim nếu thành công
            }

            return new List<PhimModel>(); // Trả về danh sách rỗng nếu thất bại
        }
    }
}
