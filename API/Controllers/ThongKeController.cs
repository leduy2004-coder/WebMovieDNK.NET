using API.Dto;
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

        [HttpGet("ThongKe/{nam}")]
        public async Task<IActionResult> GetVeBanTungThang(string nam)
        {
            var data = await _thongKeRepository.GetVeBanTungThang(nam);
            var data1 = await _thongKeRepository.GetTongTienTheoNam(nam);
            var result1 = await _thongKeRepository.GetTopCustomersByYear(nam);
            var result2 = await _thongKeRepository.GetTongVeTrongNam(nam);
            var result3 = await _thongKeRepository.GetSoLuongPhimDaChieuTrongNam(nam);


            var model = new ThongKeDTO
            {
                SoLuongPhimDaChieuTrongNam = result3,
                TongTienTheoNam = data1,
                TongVeTrongNam = result2,
                VeBanTungThang = data,
                topCustomerDTOs = result1.ToList(),
            };

            return Ok(model);
        }
    }
}
