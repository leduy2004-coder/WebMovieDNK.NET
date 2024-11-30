using Microsoft.AspNetCore.Mvc;
using API.Model;
using API.Data;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
       
        private readonly IKhachHangRepository khachHangRepository;

        public LoginController(IKhachHangRepository khachHangRepository)
        {
            this.khachHangRepository = khachHangRepository;
        }

        // Đăng nhập khách hàng
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            // Kiểm tra nếu dữ liệu không hợp lệ
            if (request == null || string.IsNullOrEmpty(request.TenTK) || string.IsNullOrEmpty(request.MatKhau))
            {
                return BadRequest("Email và mật khẩu không được để trống.");
            }

            // Gọi phương thức Login từ repository để xác thực thông tin đăng nhập
            var khachHang = await khachHangRepository.Login(request.TenTK, request.MatKhau);

            if (khachHang == null)
            {
                return Unauthorized("Tài khoản hoặc mật khẩu không chính xác.");
            }

            // Trả về kết quả nếu đăng nhập thành công
            return Ok(khachHang);  // Có thể trả về thông tin khách hàng hoặc token nếu có
        }

        [HttpPost("register")]
        public async Task<bool> RegisterAsync([FromBody] RegisterRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request.TenTK) ||
                string.IsNullOrEmpty(request.MatKhau) || string.IsNullOrEmpty(request.Sdt) ||
                string.IsNullOrEmpty(request.Email))
            {
                return false;
            }

            var khachHang = new tbKhachHang
            {
                HoTen = request.hoTen,
                Sdt = request.Sdt,
                Email = request.Email,
                TenTK = request.TenTK,
                MatKhau = BCrypt.Net.BCrypt.HashPassword(request.MatKhau),
                // Không cần gán maKH vì trigger sẽ tự động sinh maKH
            };

            khachHang.MaKH = "";

              var result = await khachHangRepository.RegisterAsync(khachHang);

            if ( result == false)
            {
                return false;
            }

            return true;
        }
        public class LoginRequest
        {
            public string TenTK { get; set; }
            public string MatKhau { get; set; }
        }
        public class RegisterRequest
        {
            public string hoTen { get; set; }
            public string TenTK { get; set; }
            public string MatKhau { get; set; }
            public string Email { get; set; }
            public string Sdt { get; set; }
        }

    }
}
