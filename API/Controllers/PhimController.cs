using Microsoft.AspNetCore.Mvc;
using API.Model;
using API.Data;
using API.Dto;
using Mapster;

namespace KTGiuaKi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhimController : ControllerBase
    {
        private readonly IPhimRepository _phimRepository;
        private readonly ISuatChieuRepository _suatChieuRepository;
        private readonly CloudinaryService _cloudinary;

        public PhimController(IPhimRepository phimRepository, ISuatChieuRepository suatChieuRepository, CloudinaryService cloudinary)
        {
            _phimRepository = phimRepository;
            _suatChieuRepository = suatChieuRepository;
            _cloudinary = cloudinary;
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

        // GET: api/Phim/all/status
        [HttpGet("all/status")]
        public async Task<IActionResult> GetPHIMStatus()
        {
            try
            {
                var result = await _phimRepository.GetPHIMStatus();

                return Ok(result);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data.");
            }
        }
        // GET: api/TLPhim/all
        [HttpGet("all-type")]
        public async Task<IActionResult> GetTLPHIMS()
        {
            try
            {
                var result = await _phimRepository.GetTheLoaiPHIMs();

                return Ok(result);
            }
            catch (Exception ex)
            {
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
                var resultMovie = await _phimRepository.GetThongTinPhim(maPhim);

                await _cloudinary.DeleteImageBySecureUrlAsync(resultMovie.HinhDaiDien);

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
        public async Task<ActionResult<PhimDTO>> CreatePhim([FromForm] PhimDTO phim, [FromForm] IFormFile HinhDaiDienFile)
        {
            if (phim == null)
                return BadRequest(ModelState);
            if (HinhDaiDienFile != null && HinhDaiDienFile.Length > 0)
            {
                // Giả sử bạn sử dụng Cloudinary để tải ảnh lên
                var imageUrl = await _cloudinary.UploadImageAsync(HinhDaiDienFile);
                phim.HinhDaiDien = imageUrl; 
            }

            var tbPhim = phim.Adapt<tbPhim>();
            try
            {
                var createdPhim = await _phimRepository.AddPHIM(tbPhim);
                return Ok(createdPhim);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error creating data.");
            }
        }

        // PUT: api/Phim
        [HttpPut]
        public async Task<ActionResult<PhimDTO>> UpdatePhim([FromForm] PhimDTO phim, [FromForm] IFormFile? HinhDaiDienFile)
        {
 
            if (HinhDaiDienFile != null && HinhDaiDienFile.Length > 0)
            {
                if (!string.IsNullOrEmpty(phim.HinhDaiDien))
                {
                    await _cloudinary.DeleteImageBySecureUrlAsync(phim.HinhDaiDien);
                }
                // Giả sử bạn sử dụng Cloudinary để tải ảnh lên
                var imageUrl = await _cloudinary.UploadImageAsync(HinhDaiDienFile);
                phim.HinhDaiDien = imageUrl;
            }
            var tbPhim = phim.Adapt<tbPhim>();
            try
            {
                var updatedPhim = await _phimRepository.UpdatePHIM(tbPhim);
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

        [HttpGet("Tim/{tenPhim}")]
        public async Task<IActionResult> GetTimPhim(string tenPhim)
        {
            var result = await _phimRepository.GetTimPhim(tenPhim);

            return Ok(result);
        }
    }
}
