using API.Data;
using API.Dto;
using API.Model;
using Mapster;
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
        private readonly IKhachHangRepository _khachHangRepository;

        public BinhLuanController(IBinhLuanRepository binhLuanRepository, IKhachHangRepository khachHangRepository)
        {
            _binhLuanRepository = binhLuanRepository;
            _khachHangRepository = khachHangRepository;
        }

        // Thêm bình luận
        [HttpPost]
        public async Task<ActionResult<BinhLuanDTO>> CreateBinhLuan(tbBinhLuan bl)
        {
            if (bl == null)
            {
                return BadRequest("Thông tin bình luận không hợp lệ.");
            }

            var createdBinhLuan = await _binhLuanRepository.AddBinhLuan(bl);
            // Sử dụng Mapper để chuyển đổi sang DTO
            var binhLuanDTO = createdBinhLuan.Adapt<BinhLuanDTO>();
            binhLuanDTO.KhachHang = await _khachHangRepository.GetKhachHangById(createdBinhLuan.MaKH);
            return Ok(binhLuanDTO);
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

        [HttpGet("all/{maPhim}")]
        public async Task<ActionResult<IEnumerable<BinhLuanDTO>>> GetAllBinhLuan(string maPhim)
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

            // Chuyển đổi các thực thể sang DTO và thêm thông tin khách hàng
            var binhLuanDTOs = new List<BinhLuanDTO>();
            foreach (var binhLuan in binhLuans)
            {
                var binhLuanDTO = binhLuan.Adapt<BinhLuanDTO>();
                binhLuanDTO.KhachHang = await _khachHangRepository.GetKhachHangById(binhLuan.MaKH);
                binhLuanDTOs.Add(binhLuanDTO);
            }

            return Ok(binhLuanDTOs);
        }


        // Lấy thông tin bình luận theo mã
        [HttpGet("{maBinhLuan}")]
        public async Task<ActionResult<BinhLuanDTO>> GetBinhLuan(int maBinhLuan)
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
