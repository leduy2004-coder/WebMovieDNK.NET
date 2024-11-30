using Microsoft.AspNetCore.Mvc;
using API.Model;
using API.Data;

namespace KTGiuaKi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhimController : ControllerBase
    {
        private readonly IPhimRepository _phimRepository;
        private readonly ISuatChieuRepository _suatChieuRepository;

        public PhimController(IPhimRepository phimRepository, ISuatChieuRepository suatChieuRepository)
        {
            _phimRepository = phimRepository;
            _suatChieuRepository = suatChieuRepository;
        }

        // GET: api/Phim/all
        [HttpGet("all")]
        public async Task<IActionResult> GetPHIMS()
        {
            try
            {
                var result = await _phimRepository.GetPHIMs();
                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET: api/Phim/dang-chieu
        [HttpGet("dang-chieu")]
        public async Task<IActionResult> GetPHIMDANGCHIEU()
        {
            try
            {
                var result = await _phimRepository.GetPhimDangChieu();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET: api/Phim/sap-chieu
        [HttpGet("sap-chieu")]
        public async Task<IActionResult> GetPHIMSAPCHIEU()
        {
            try
            {
                var result = await _phimRepository.GetPhimChuaChieu();
                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // GET: api/Phim/{maPhim}
        [HttpGet("{maPhim}")]
        public async Task<IActionResult> GetPHIM(string maPhim)
        {
            try
            {
                var result = await _phimRepository.GetThongTinPhim(maPhim);
                if (result == null)
                    return NotFound($"Phim with ID {maPhim} not found.");

                return Ok(result);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }

        // DELETE: api/Phim/{maPhim}
        [HttpDelete("{maPhim}")]
        public async Task<IActionResult> DeletePHIM(string maPhim)
        {
            try
            {
                var result = await _phimRepository.DeletePHIM(maPhim);
                if (!result)
                    return NotFound($"Phim with ID {maPhim} not found.");

                return Ok($"Phim with ID {maPhim} has been deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error deleting data.");
            }
        }

        // POST: api/Phim
        [HttpPost]
        public async Task<IActionResult> CreatePhim([FromBody] tbPhim phim)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var createdPhim = await _phimRepository.AddPHIM(phim);
                return CreatedAtAction(nameof(GetPHIM), new { maPhim = createdPhim.MaPhim }, createdPhim);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
        }

        // PUT: api/Phim
        [HttpPut]
        public async Task<IActionResult> UpdatePhim([FromBody] tbPhim phim)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedPhim = await _phimRepository.UpdatePHIM(phim);
                if (updatedPhim == null)
                    return NotFound($"Phim with ID {phim.MaPhim} not found.");

                return Ok(updatedPhim);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error updating data.");
            }
        }

        [HttpGet("ngayxem/{phimId}")]
        public async Task<IActionResult> GetNgayXem(string phimId)
        {
            var ngayXem = await _suatChieuRepository.GetNgayChieuTheoPhim(phimId);

            // Chuyển đổi DateTime sang string với định dạng dd/MM/yyyy
            var formattedNgayXem = ngayXem.Select(n => n.ToString("dd/MM/yyyy"));

            return Ok(formattedNgayXem.ToList());
        }

    }
}
