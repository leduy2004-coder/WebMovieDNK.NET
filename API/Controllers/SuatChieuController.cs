using Microsoft.AspNetCore.Mvc;
using API.Model;
using API.Data;
using KTGiuaKi.Dto;

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

        [HttpGet("laysuatchieu/{maSC}")]
        public async Task<IActionResult> GetSuatChieuById(string maSC)
        {
            var suatChieu = await _suatChieuRepository.GetSuatChieuTheoMa(maSC);

            suatChieu.Phim = await _phimRepository.GetThongTinPhim(suatChieu.MaPhim);
            suatChieu.CaChieu = await _suatChieuRepository.GetCaChieuTheoMaCa(suatChieu.MaCa);

            //var sanPhamDTOs = sanPhams.Adapt<List<MENUDTO>>();
            //return Ok(sanPhamDTOs);
            return Ok(suatChieu);
        }

        [HttpGet("laysoghedadat/{maSC}")]
        public async Task<IActionResult> GetChairBooked(string maSC)
        {
            var gheDaDat = await _suatChieuRepository.GetGheDaDat(maSC);
            return Ok(gheDaDat);
        }
    }
}
