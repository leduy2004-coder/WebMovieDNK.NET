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

        [HttpGet("VeBanTungThang/{nam}")]
        public async Task<IActionResult> GetVeBanTungThang(string nam)
        {
            var data = await _thongKeRepository.GetVeBanTungThang(nam);
            return Ok(data);
        }

        [HttpGet("TongTienTrongNam/{nam}")]
        public async Task<IActionResult> GetTongTienTrongNam(string nam)
        {
            var data = await _thongKeRepository.GetTongTienTheoNam(nam);
            return Ok(data);
        }

        [HttpGet("TopCustomers/{nam}")]
        public async Task<IActionResult> GetTopCustomers(string nam)
        {
            var result = await _thongKeRepository.GetTopCustomersByYear(nam);
            return Ok(result);
        }

        [HttpGet("TongVeTrongNam/{nam}")]
        public async Task<IActionResult> GetTongVeTrongNam(string nam)
        {
            var result = await _thongKeRepository.GetTongVeTrongNam(nam);
            return Ok(result);
        }
         
        [HttpGet("SoLuongPhimDaChieuTrongNam/{nam}")]
        public async Task<IActionResult> GetSoLuongPhimDaChieuTrongNam(string nam)
        {
            var result = await _thongKeRepository.GetSoLuongPhimDaChieuTrongNam(nam);
            return Ok(result);
        }
    }
}
