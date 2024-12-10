using API.Data;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IKhachHangRepository _khachHangRepository;

        public KhachHangController(IKhachHangRepository khachHangRepository)
        {
            _khachHangRepository = khachHangRepository;
        }

        // Lấy danh sách tất cả khách hàng
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tbKhachHang>>> GetDanhSachKhachHang()
        {
            var khachHangList = await _khachHangRepository.GetDanhSachKhachHang();
            return Ok(khachHangList); // Trả về danh sách khách hàng
        }

        // Lấy thông tin khách hàng theo mã
        [HttpGet("{maKhachHang}")]
        public async Task<ActionResult<tbKhachHang>> GetKhachHangById(string maKhachHang)
        {
            var khachHang = await _khachHangRepository.GetKhachHangById(maKhachHang);
            if (khachHang == null)
            {
                return NotFound("Không tìm thấy khách hàng."); // Trả về lỗi 404 nếu không tìm thấy
            }

            return Ok(khachHang); // Trả về khách hàng
        }

        // Thêm mới khách hàng
        [HttpPost]
        public async Task<ActionResult<tbKhachHang>> AddKhachHang(tbKhachHang kh)
        {
            if (kh == null)
            {
                return BadRequest("Thông tin khách hàng không hợp lệ."); // Trả về lỗi 400 nếu dữ liệu không hợp lệ
            }
            try
            {
                var createdKhachHang = await _khachHangRepository.AddKhachHang(kh);
                return Ok(createdKhachHang);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Lỗi khi thêm nhân viên: {ex.Message}");
            }
        }

        // Cập nhật thông tin khách hàng
        [HttpPut("{maKhachHang}")]
        public async Task<ActionResult<tbKhachHang>> UpdateKhachHang(string maKhachHang, tbKhachHang kh)
        {
            if (kh.MaKH != maKhachHang)
            {
                return BadRequest("Mã khách hàng không khớp."); // Trả về lỗi 400 nếu mã không khớp
            }

            var updatedKhachHang = await _khachHangRepository.UpdateKhachHang(kh);
            if (updatedKhachHang == null)
            {
                return NotFound("Không tìm thấy khách hàng."); // Trả về lỗi 404 nếu không tìm thấy
            }

            return Ok(updatedKhachHang); // Trả về khách hàng đã cập nhật
        }

        // Xóa khách hàng
        [HttpDelete("{maKhachHang}")]
        public async Task<ActionResult> DeleteKhachHang(string maKhachHang)
        {
            var result = await _khachHangRepository.DeleteKhachHang(maKhachHang);
            if (!result)
            {
                return NotFound("Không tìm thấy khách hàng."); // Trả về lỗi 404 nếu không tìm thấy
            }

            return NoContent(); // Trả về 204 nếu xóa thành công
        }

        [HttpGet("LichSu/{maKH}")]
        public async Task<IActionResult> GetLSKhachHang(string maKH)
        {
            var lichSu = await _khachHangRepository.GetLSKhachHang(maKH);


            return Ok(lichSu); // Trả về danh sách lịch sử
        }

        [HttpGet("Tim/{tenKH}")]
        public async Task<IActionResult> GetTimKH(string tenKH)
        {
            var result = await _khachHangRepository.GetTimKH(tenKH);

            return Ok(result);
        }
    }
}
