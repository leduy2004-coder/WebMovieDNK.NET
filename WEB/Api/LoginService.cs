
using API.Data;
using Newtonsoft.Json;
using System.Text;
using Web.Api;
using WEB.Models;
using System.Threading.Tasks;
namespace WEB.Api
{
    public class LoginService
    {
        private readonly ApiService _apiService;
        public LoginService(ApiService apiService)
        {
            _apiService = apiService;
        }


      
        public async Task<KhachHangModel> LoginAsync(KhachHangModel loginRequest)
        {
            // Gửi yêu cầu POST đến API đăng nhập
            var response = await _apiService.PostDataAsync<KhachHangModel>("/api/login/login", loginRequest);

            // Kiểm tra kết quả từ API
            if (response != null )
            {
                return response; // Đăng nhập thành công
            }

            return null;
        }
    }
}
