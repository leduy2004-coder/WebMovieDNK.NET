
using Microsoft.AspNetCore.Mvc;
using Web.Api;
using WEB.Api;
using WEB.Models;
using static WEB.Api.LoginService;

namespace WEB.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        private readonly LoginService _loginService;
        public LoginController(LoginService loginService)
        {
            this._loginService = loginService;
        }

        //login
        [HttpPost("login")]
        public async Task<IActionResult> Login(CustomerModel loginRequest)
        {
            try
            {
                var loginResponse = await _loginService.LoginAsync(loginRequest);

                if (loginResponse != null)
                {
                    
                    HttpContext.Session.SetString("UserName", loginResponse.TenTK);
                    HttpContext.Session.SetString("UserId", loginResponse.MaKH);
                    HttpContext.Session.SetString("UserEmail", loginResponse.Email);

                    TempData["SuccessMessage"] = "Đăng nhập thành công!";
                    return RedirectToAction("Index", "Home");
                }
        
                
            }
            catch (HttpRequestException ex)
            {
                // API trả về lỗi 401
                TempData["ErrorMessage"] = "Email hoặc mật khẩu không chính xác!";
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khác
                TempData["ErrorMessage"] = "Đã xảy ra lỗi. Vui lòng thử lại sau!";
            }

            return View("login");
        }

        //register
        [HttpPost("register")]
        public async Task<IActionResult> Register(CustomerModel regisRequest)
        {
            if (regisRequest == null || string.IsNullOrWhiteSpace(regisRequest.TenTK) ||
                string.IsNullOrWhiteSpace(regisRequest.MatKhau) ||
                string.IsNullOrWhiteSpace(regisRequest.Email))
            {
                return BadRequest("Thông tin đăng ký không đầy đủ.");
            }

            var result = await _loginService.RegisterAsync(regisRequest);

            if (!result)
            {
                return Conflict("Tên tài khoản hoặc email đã tồn tại.");
            }

            TempData["SuccessMessage"] = "Đăng ký thành công!";
            return RedirectToAction("Index", "Home");
        }
    

        [HttpGet("loginView")]
        public IActionResult loginView()
        {
            return View("Login");
        }

        [HttpGet("registerView")]
        public IActionResult RegisterView()
        {
            return View("Register");
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Đã đăng xuất thành công!";
            return RedirectToAction("Index", "Home");
        }
    }
  
}
