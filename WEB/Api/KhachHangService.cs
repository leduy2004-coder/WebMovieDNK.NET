using Web.Api;
using WEB.Api;
using WEB.Models;

namespace WEB.Api
{
    public class KhachHangService
    {
        private readonly ApiService _apiService;
      
        public KhachHangService(ApiService apiService)
        {
            _apiService = apiService;
          
        }

        public async Task<KhachHangModel> GetKhachHangByIdAsync(string maKH)
        {
            

            string url = $"api/KhachHang/{maKH}";
            return await _apiService.GetDataAsync<KhachHangModel>(url);
        }

        public async Task<IEnumerable<LichSuKhachHangDTO>> GetLichSuKHAsync(string maKH)
        {


            string url = $"api/KhachHang/LichSu/{maKH}";
            Console.WriteLine($"Calling URL: {url}");
            return await _apiService.GetDataAsync<IEnumerable<LichSuKhachHangDTO>>(url);

        }
    }
}
