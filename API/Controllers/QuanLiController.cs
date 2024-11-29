using API.Data;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuanLiController : ControllerBase
    {
        private readonly IQuanLiRepository _quanLiRepository;

        // Constructor nhận vào repository qua Dependency Injection
        public QuanLiController(IQuanLiRepository quanLiRepository)
        {
            _quanLiRepository = quanLiRepository;
        }

        // Thêm mới quản lý
        [HttpPost]
        public async Task<ActionResult<tbQuanLi>> CreateQuanLi([FromBody] tbQuanLi quanLi)
        {
            if (quanLi == null)
            {
                return BadRequest("Thông tin quản lý không hợp lệ.");
            }

            var createdQuanLi = await _quanLiRepository.AddQuanLi(quanLi);
            return CreatedAtAction(nameof(GetQuanLiById), new { maQuanLy = createdQuanLi.MaQL }, createdQuanLi);
        }

        // Cập nhật thông tin quản lý
        [HttpPut("{maQuanLy}")]
        public async Task<ActionResult<tbQuanLi>> UpdateQuanLi(string maQuanLy, [FromBody] tbQuanLi quanLi)
        {
            if (maQuanLy != quanLi.MaQL)
            {
                return BadRequest("Mã quản lý không khớp.");
            }

            var updatedQuanLi = await _quanLiRepository.UpdateQuanLi(quanLi);
            if (updatedQuanLi == null)
            {
                return NotFound("Không tìm thấy quản lý.");
            }

            return Ok(updatedQuanLi);
        }

        // Xóa quản lý
        [HttpDelete("{maQuanLy}")]
        public async Task<ActionResult> DeleteQuanLi(string maQuanLy)
        {
            var result = await _quanLiRepository.DeleteQuanli(maQuanLy);
            if (!result)
            {
                return NotFound("Không tìm thấy quản lý.");
            }

            return NoContent(); // Trả về 204 No Content nếu xóa thành công
        }

        // Lấy thông tin quản lý theo mã
        [HttpGet("{maQuanLy}")]
        public async Task<ActionResult<tbQuanLi>> GetQuanLiById(string maQuanLy)
        {
            var quanLi = await _quanLiRepository.GetDanhSachQuanLi();  // Lấy tất cả quản lý, bạn có thể thay đổi theo yêu cầu
            var foundQuanLi = quanLi.FirstOrDefault(q => q.MaQL == maQuanLy); // Tìm quản lý theo mã
            if (foundQuanLi == null)
            {
                return NotFound("Không tìm thấy quản lý.");
            }

            return Ok(foundQuanLi);
        }

        // Lấy tất cả quản lý
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tbQuanLi>>> GetDanhSachQuanLi()
        {
            var quanLis = await _quanLiRepository.GetDanhSachQuanLi();
            return Ok(quanLis);
        }
    }
}
