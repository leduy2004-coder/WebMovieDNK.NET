using API.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UuDaiController : ControllerBase
    {
        private readonly IUuDaiRepository _uuDaiRepository;
     
        public UuDaiController(IUuDaiRepository uuDaiRepository)
        {
            this._uuDaiRepository = uuDaiRepository;
        }

        // GET: api/Phim/all
        [HttpGet("all")]
        public async Task<IActionResult> GetPHIMS()
        {
            try
            {
                var result = await _uuDaiRepository.GetUuDais();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET: api/Phim/all
        [HttpGet("{maUuDai}")]
        public async Task<IActionResult> GetUuDaiById(string maUuDai)
        {
            try
            {
                var result = await _uuDaiRepository.GetUuDaiById(maUuDai);
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }
    }
}
