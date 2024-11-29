using API.Data;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhongChieuController : ControllerBase
    {
        private readonly IPhongChieuRepository _phongChieuRepository;

        public PhongChieuController(IPhongChieuRepository phongChieuRepository)
        {
            _phongChieuRepository = phongChieuRepository;
        }

        // Lấy danh sách tất cả phòng chiếu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tbPhongChieu>>> GetDanhSachPhongChieu()
        {
            var phongChieus = await _phongChieuRepository.GetDanhSachPhongChieu();
            return Ok(phongChieus);
        }

        // Lấy thông tin phòng chiếu
        [HttpGet("{maPhong}")]
        public async Task<ActionResult<tbPhongChieu>> GetPhongChieu(string maPhong)
        {
            var phongChieu = await _phongChieuRepository.GetPhongChieu();
            if (phongChieu == null)
            {
                return NotFound("Phòng chiếu không tìm thấy.");
            }
            return Ok(phongChieu);
        }

        // Thêm phòng chiếu mới
        [HttpPost]
        public async Task<ActionResult<tbPhongChieu>> AddPhongChieu(tbPhongChieu pc)
        {
            if (pc == null)
            {
                return BadRequest("Thông tin phòng chiếu không hợp lệ.");
            }

            var createdPhongChieu = await _phongChieuRepository.AddPhongChieu(pc);
            return CreatedAtAction(nameof(GetPhongChieu), new { maPhong = createdPhongChieu.MaPhong }, createdPhongChieu);
        }

        // Cập nhật phòng chiếu
        [HttpPut("{maPhong}")]
        public async Task<ActionResult<tbPhongChieu>> UpdatePhongChieu(string maPhong, tbPhongChieu pc)
        {
            if (maPhong != pc.MaPhong)
            {
                return BadRequest("Mã phòng không khớp.");
            }

            var updatedPhongChieu = await _phongChieuRepository.UpdatePhongChieu(pc);
            if (updatedPhongChieu == null)
            {
                return NotFound("Phòng chiếu không tìm thấy.");
            }

            return Ok(updatedPhongChieu);
        }

        // Xóa phòng chiếu
        [HttpDelete("{maPhong}")]
        public async Task<ActionResult> DeletePhongChieu(string maPhong)
        {
            var result = await _phongChieuRepository.DeletePhongChieu(maPhong);
            if (!result)
            {
                return NotFound("Phòng chiếu không tìm thấy.");
            }

            return NoContent();
        }
    }
}
