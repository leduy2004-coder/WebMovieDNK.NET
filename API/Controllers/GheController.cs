using Microsoft.AspNetCore.Mvc;
using API.Model;
using API.Data;
using KTGiuaKi.Dto;

namespace KTGiuaKi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GheController : ControllerBase
    {
        private readonly IPhimRepository _phimRepository;
        private readonly ISuatChieuRepository _suatChieuRepository;

        public GheController(IPhimRepository phimRepository, ISuatChieuRepository suatChieuRepository)
        {
            _phimRepository = phimRepository;
            _suatChieuRepository = suatChieuRepository;
        }


        // GET: api/ghe/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAllChair()
        {
            try
            {
                var result = await _suatChieuRepository.GetAllGhe();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        [HttpGet("laysoghedadat/{maSC}")]
        public async Task<IActionResult> GetChairBooked(string maSC)
        {
            var gheDaDat = await _suatChieuRepository.GetGheDaDat(maSC);
            return Ok(gheDaDat);
        }
    }
}
