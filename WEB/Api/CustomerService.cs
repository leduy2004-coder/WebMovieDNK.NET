using Web.Api;
using WEB.Api;
using WEB.Models;

namespace WEB.Api
{
    public class CustomerService
    {
        private readonly ApiService _apiService;
      
        public CustomerService(ApiService apiService)
        {
            _apiService = apiService;
          
        }

        public async Task<CustomerModel> GetKhachHangByIdAsync(string maKH)
        {
            

            string url = $"api/KhachHang/{maKH}";
            return await _apiService.GetDataAsync<CustomerModel>(url);
        }

        public async Task<IEnumerable<LichSuKhachHangDTO>> GetLichSuKHAsync(string maKH)
        {


            string url = $"api/KhachHang/LichSu/{maKH}";
            Console.WriteLine($"Calling URL: {url}");
            return await _apiService.GetDataAsync<IEnumerable<LichSuKhachHangDTO>>(url);

        }
    }
}
