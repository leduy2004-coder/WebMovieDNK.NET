

using Newtonsoft.Json;
using System.Text;
using Web.Api;
using WEB.Models;
using System.Threading.Tasks;
using Web.Models;
namespace WEB.Api
{
    public class LoginService
    {
        private readonly ApiService _apiService;
        public LoginService(ApiService apiService)
        {
            _apiService = apiService;
        }


      
        public async Task<LoginModel> LoginAsync(CustomerModel loginRequest)
        {
            // Gửi yêu cầu POST đến API đăng nhập
            var response = await _apiService.PostDataAsync<LoginModel>("/api/login/login", loginRequest);

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

        public async Task<bool> SendCode(string email)
        {
            try
            {
                // Gửi dữ liệu dưới dạng đối tượng JSON thay vì chuỗi đơn giản
                
                var response= await _apiService.PostDataAsync<bool>("api/login/sendCode", email);
                return response;

            }
            catch (Exception ex)
            {
                throw new Exception($"Không thể gửi mã xác nhận: {ex.Message}");
            }
        }

        public async Task<bool> Verify(string email, string code)
        {
            try
            {
                // Đóng gói email và code vào một object
                var requestData = new { email, code };

                // Gửi request đến API
                return await _apiService.PostDataAsync<bool>("api/login/verify", requestData);
            }
            catch (Exception ex)
            {
                throw new Exception($"Không thể xác thực mã: {ex.Message}");
            }
        }
    }
}
