using API.Data;
using Microsoft.AspNetCore.Mvc;
using Web.Api;
using WEB.Api;
using static WEB.Api.LoginService;

namespace WEB.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        private readonly LoginService loginService;
        public LoginController(LoginService loginService)
        {
            this.loginService = loginService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(KhachHangModel loginRequest)
        {
            try
            {
                var loginResponse = await loginService.LoginAsync(loginRequest);

                if (loginResponse != null)
                {
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

            return View("Index");
        }

        [HttpGet("Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
  
}
