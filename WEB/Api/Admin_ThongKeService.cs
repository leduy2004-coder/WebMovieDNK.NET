using Newtonsoft.Json;
using System.Collections.Generic;
using System.Threading.Tasks;
using WEB.Models;
using Web.Api;
using System.Xml.Linq;

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
        public async Task<Admin_HomeView> GetThongKeAsync(string nam)
        {
            string url = $"api/thongke/thongke/{nam}"; 

            var response = await _apiService.GetDataAsync<Admin_HomeView>(url);

            return response; 
        }
    }
}
