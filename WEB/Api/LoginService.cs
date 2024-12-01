

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


      
        public async Task<CustomerModel> LoginAsync(CustomerModel loginRequest)
        {
            // Gửi yêu cầu POST đến API đăng nhập
            var response = await _apiService.PostDataAsync<CustomerModel>("/api/login/login", loginRequest);

            // Kiểm tra kết quả từ API
            if (response != null )
            {
                return response; // Đăng nhập thành công
            }

            return null;
        }

        public async Task<bool> RegisterAsync(CustomerModel regisRequest)
        {
            try
            {
                var response = await _apiService.PostDataAsync<bool>("/api/login/register", regisRequest);
                return response; // Đảm bảo trả về kết quả nếu không có lỗi
            }
            catch (Exception ex)
            {
                // Xử lý lỗi (nếu cần)
                Console.WriteLine($"Error: {ex.Message}");
                return false; // Trả về false khi có lỗi
            }

        }
    }
}
