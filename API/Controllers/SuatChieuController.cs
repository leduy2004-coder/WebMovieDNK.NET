using Microsoft.AspNetCore.Mvc;
using API.Model;
using API.Data;

namespace KTGiuaKi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SuatChieuController : ControllerBase
    {
        private readonly IPhimRepository _phimRepository;
        private readonly ISuatChieuRepository _suatChieuRepository;

        public SuatChieuController(IPhimRepository phimRepository, ISuatChieuRepository suatChieuRepository)
        {
            _phimRepository = phimRepository;
            _suatChieuRepository = suatChieuRepository;
        }


        [HttpGet("{phimId}/{ngay}")]
        public async Task<IActionResult> GetSuatChieu(string phimId, string ngay)
        {
            var suatChieu = await _suatChieuRepository.GetCaChieuTheoPhimVaNgay(phimId, ngay);
            return Ok(suatChieu);
        }

    }
}
