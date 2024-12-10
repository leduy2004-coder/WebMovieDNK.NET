
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
                    HttpContext.Session.SetString("FullName", loginResponse.HoTen);
                    HttpContext.Session.SetString("UserId", loginResponse.Id);
                    HttpContext.Session.SetString("UserEmail", loginResponse.Email);
                    HttpContext.Session.SetString("Role", loginResponse.Role);

                   
                    if (loginResponse.Role == "ADMIN")
                    {
                        return RedirectToAction("Index", "Admin_Home");
                    }else if (loginResponse.Role == "USER")
                    {
                        TempData["Message"] = "Đăng nhập thành công!";
                        TempData["MessageType"] = "success";
                        return RedirectToAction("Index", "Home");
                    }

                }

            }
            catch (HttpRequestException ex)
            {
                // API trả về lỗi 401
                TempData["Message"] = "Email hoặc mật khẩu không chính xác!";
                TempData["MessageType"] = "error";
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khác
                TempData["Message"] = "Đã xảy ra lỗi. Vui lòng thử lại sau!";
                TempData["MessageType"] = "error";
            }

            return View("login");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(CustomerModel model)
        {
            // Gửi mã xác nhận
            var isSent = await _loginService.SendCode(model.Email);
            if (isSent)
            {
                TempData["Message"] = "Mã xác nhận đã được gửi đến email của bạn.";
                TempData["MessageType"] = "success";

                return RedirectToAction("Confirm", model);
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi gửi mã xác nhận.";
                return View(model);
            }
        }


        [HttpGet("send-code")]
        public async Task<bool> SendCode(string email)
        {
            var isSent = await _loginService.SendCode(email);
            if (isSent)
            {
                TempData["Message"] = "Mã xác nhận đã được gửi lại đến email của bạn.";
                TempData["MessageType"] = "success";

                return true;
            }
            else
            {
                TempData["ErrorMessage"] = "Có lỗi xảy ra khi gửi lại mã xác nhận.";
                return false;
            }
        }

        [HttpPost("Verify")]
        public async Task<IActionResult> VerifyCode(CustomerModel model)
        {
            if (string.IsNullOrEmpty(model.VerificationCode))
            {
                TempData["ErrorMessage"] = "Mã xác nhận không được để trống.";
                return View(model);
            }

            var isValidCode = await _loginService.Verify(model.Email, model.VerificationCode);
            if (!isValidCode)
            {
                TempData["ErrorMessage"] = "Mã xác nhận không hợp lệ.";
                return View("Confirm", model);
            }

            // Bước 3: Đăng ký tài khoản
            var isRegistered = await _loginService.RegisterAsync(model);
            if (isRegistered)
            {
                TempData["Message"] = "Đăng ký thành công!";
                TempData["MessageType"] = "success";

                return View("Login");  // Chuyển hướng đến trang đăng nhập
            }

            TempData["ErrorMessage"] = "Có lỗi xảy ra khi đăng ký tài khoản.";
            return RedirectToAction("Confirm", model);
        }


        [HttpGet("loginView")]
        public IActionResult loginView()
        {
            var role = HttpContext.Session.GetString("Role");
            if (role == "ADMIN")
            {
                HttpContext.Session.Clear();
                return RedirectToAction("loginView", "Login");
            }
            else
                return View("Login");
        }

        [HttpGet("registerView")]
        public IActionResult RegisterView()
        {
            return View("Register");
        }

        [HttpGet("confirm")]
        public IActionResult Confirm(CustomerModel model)
        {

            return View(model);
        }

        [HttpGet("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            TempData["Message"] = "Đã đăng xuất thành công!";
            TempData["MessageType"] = "success";
            return RedirectToAction("loginView", "Login");
        }
    }

}