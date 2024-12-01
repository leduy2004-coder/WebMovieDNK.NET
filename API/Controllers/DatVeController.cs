using API.Data;
using API.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatVeController : ControllerBase
    {
        private readonly IDatVeRepository _datVeRepository;

        public DatVeController(IDatVeRepository datVeRepository)
        {
            _datVeRepository = datVeRepository;
        }

        // Thêm mới BookVe
        [HttpPost("bookve")]
        public async Task<ActionResult<tbBookVe>> AddBookVe([FromBody] tbBookVe bookVe)
        {
            if (bookVe == null)
            {
                return BadRequest("Thông tin book ve không hợp lệ.");
            }
            bookVe.MaNV = null;
            var createdBookVe = await _datVeRepository.ThemMoiBookVe(bookVe);
            return Ok(createdBookVe);
        }

        // Thêm mới BookGhe
        [HttpPost("bookghe")]
        public async Task<ActionResult<tbBookGhe>> AddBookGhe([FromBody] tbBookGhe bookGhe)
        {
            if (bookGhe == null)
            {
                return BadRequest("Thông tin book ghe không hợp lệ.");
            }

            var createdBookGhe = await _datVeRepository.ThemMoiBookGhe(bookGhe);
            return Ok(createdBookGhe);
        }



    }
}
