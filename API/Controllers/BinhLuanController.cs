using API.Data;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BinhLuanController : ControllerBase
    {
        private readonly IBinhLuanRepository _binhLuanRepository;

        public BinhLuanController(IBinhLuanRepository binhLuanRepository)
        {
            _binhLuanRepository = binhLuanRepository;
        }

        // Thêm bình luận
        [HttpPost]
        public async Task<ActionResult<tbBinhLuan>> CreateBinhLuan(tbBinhLuan bl)
        {
            if (bl == null)
            {
                return BadRequest("Thông tin bình luận không hợp lệ.");
            }

            var createdBinhLuan = await _binhLuanRepository.AddBinhLuan(bl);
            return CreatedAtAction(nameof(GetBinhLuan), new { maBinhLuan = createdBinhLuan.MaBinhLuan }, createdBinhLuan);
        }

        // Cập nhật bình luận
        [HttpPut("{maBinhLuan}")]
        public async Task<ActionResult<tbBinhLuan>> UpdateBinhLuan(int maBinhLuan, tbBinhLuan bl)
        {
            if (bl.MaBinhLuan != maBinhLuan)
            {
                return BadRequest("Mã bình luận không khớp.");
            }

            var updatedBinhLuan = await _binhLuanRepository.UpdateBinhLuan(bl);
            if (updatedBinhLuan == null)
            {
                return NotFound("Không tìm thấy bình luận.");
            }

            return Ok(updatedBinhLuan);
        }

        // Xóa bình luận
        [HttpDelete("{maBinhLuan}")]
        public async Task<ActionResult> DeleteBinhLuan(int maBinhLuan)
        {
            var result = await _binhLuanRepository.DeleteBinhLuan(maBinhLuan.ToString());
            if (!result)
            {
                return NotFound("Không tìm thấy bình luận.");
            }

            return NoContent();
        }

        [HttpGet("{maPhim}")]
        public async Task<ActionResult<IEnumerable<tbBinhLuan>>> GetAllBinhLuan(string maPhim)
        {
            if (string.IsNullOrEmpty(maPhim))
            {
                return BadRequest("Mã phim không được để trống.");
            }

            var binhLuans = await _binhLuanRepository.GetAllBinhLuan(maPhim);

            if (binhLuans == null || !binhLuans.Any())
            {
                return NotFound($"Không tìm thấy bình luận nào cho mã phim: {maPhim}.");
            }

            return Ok(binhLuans);
        }



        // Lấy thông tin bình luận theo mã
        [HttpGet("{maBinhLuan}")]
        public async Task<ActionResult<tbBinhLuan>> GetBinhLuan(int maBinhLuan)
        {
            var binhLuan = await _binhLuanRepository.GetBinhLuan(maBinhLuan.ToString());
            if (binhLuan == null)
            {
                return NotFound("Không tìm thấy bình luận.");
            }

            return Ok(binhLuan);
        }
    }
}
