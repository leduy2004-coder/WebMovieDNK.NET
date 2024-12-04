using API.Data;
using API.Model;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DatDoUongController : ControllerBase
    {
        private readonly IDatDoUongRepository _datDoUongRepository;

        public DatDoUongController(IDatDoUongRepository datDoUong)
        {
            _datDoUongRepository = datDoUong;
        }

        // Thêm mới BookVe
        [HttpPost("datdouong")]
        public async Task<ActionResult<tbBookDoUong>> AddBookDrink([FromBody] tbBookDoUong bookDoUong)
        {
            if (bookDoUong == null)
            {
                return BadRequest("Thông tin book không hợp lệ.");
            }
            var createBook = await _datDoUongRepository.DatDoUong(bookDoUong);
            return Ok(createBook);
        }


        [HttpGet("laythongtindouong")]
        public async Task<IActionResult> GetInfoDrinks()
        {
            var ThongTin = await _datDoUongRepository.LayThongTinDoUong();
            return Ok(ThongTin);
        }

    }
}
