using API.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ThongKeController : ControllerBase
    {
        private readonly IThongKeRepository _thongKeRepository;

        public ThongKeController(IThongKeRepository thongKeRepository)
        {
            _thongKeRepository = thongKeRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetThongKe()
        {
            var data = await _thongKeRepository.ThongKe();
            return Ok(data);
        }
    }
}
