using API.Data;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeController : ControllerBase
    {
        private readonly IVeRepository _veRepository;

        public VeController(IVeRepository veRepository)
        {
            _veRepository = veRepository;
        }

        // Thêm vé
        [HttpPost]
        public async Task<ActionResult<tbVe>> AddVe(tbVe ve)
        {
            if (ve == null)
            {
                return BadRequest("Thông tin vé không hợp lệ.");
            }

            var createdVe = await _veRepository.AddVe(ve);
            return CreatedAtAction(nameof(GetVe), new { maVe = createdVe.MaVe }, createdVe);
        }

        // Cập nhật vé
        [HttpPut("{maVe}")]
        public async Task<ActionResult<tbVe>> UpdateVe(string maVe, tbVe ve)
        {
            if (maVe != ve.MaVe)
            {
                return BadRequest("Mã vé không khớp.");
            }

            var updatedVe = await _veRepository.UpdateVe(ve);
            if (updatedVe == null)
            {
                return NotFound("Vé không tìm thấy.");
            }

            return Ok(updatedVe);
        }

        // Xóa vé
        [HttpDelete("{maVe}")]
        public async Task<ActionResult> DeleteVe(string maVe)
        {
            var result = await _veRepository.DeleteVe(maVe);
            if (!result)
            {
                return NotFound("Vé không tìm thấy.");
            }

            return NoContent(); // Trả về 204 No Content nếu xóa thành công
        }

        // Lấy danh sách vé
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tbVe>>> GetDanhSachVe()
        {
            var veList = await _veRepository.GetDanhSachVe();
            return Ok(veList);
        }

        // Lấy thông tin vé theo mã phim
        [HttpGet("{maPhim}")]
        public async Task<ActionResult<tbVe>> GetVe(string maPhim)
        {
            var ve = await _veRepository.GetThongTinVe(maPhim);  

            return Ok(ve);
        }
    }
}
