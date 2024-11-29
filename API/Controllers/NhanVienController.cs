using API.Data;
using API.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NhanVienController : ControllerBase
    {
        private readonly INhanVienRepository _nhanVienRepository;

        public NhanVienController(INhanVienRepository nhanVienRepository)
        {
            _nhanVienRepository = nhanVienRepository;
        }

        // Thêm mới nhân viên
        [HttpPost]
        public async Task<ActionResult<tbNhanVien>> CreateNhanVien(tbNhanVien nv)
        {
            if (nv == null)
            {
                return BadRequest("Thông tin nhân viên không hợp lệ.");
            }

            var createdNhanVien = await _nhanVienRepository.AddNhanVien(nv);
            return CreatedAtAction(nameof(GetNhanVien), new { maNhanVien = createdNhanVien.MaNV }, createdNhanVien);
        }

        // Cập nhật thông tin nhân viên
        [HttpPut("{maNhanVien}")]
        public async Task<ActionResult<tbNhanVien>> UpdateNhanVien(string maNhanVien, tbNhanVien nv)
        {
            if (nv == null)
            {
                return BadRequest("Dữ liệu nhân viên không hợp lệ.");
            }

            if (nv.MaNV != maNhanVien)
            {
                return BadRequest("Mã nhân viên không khớp.");
            }

            var updatedNhanVien = await _nhanVienRepository.UpdateNhanVien(nv);
            if (updatedNhanVien == null)
            {
                return NotFound("Không tìm thấy nhân viên.");
            }

            return Ok(updatedNhanVien);
        }

        // Xóa nhân viên
        [HttpDelete("{maNhanVien}")]
        public async Task<ActionResult> DeleteNhanVien(string maNhanVien)
        {
            var result = await _nhanVienRepository.DeleteNhanVien(maNhanVien);
            if (!result)
            {
                return NotFound("Không tìm thấy nhân viên.");
            }

            return NoContent(); // Trả về 204 No Content nếu xóa thành công
        }

        // Lấy thông tin nhân viên theo mã
        [HttpGet("{maNhanVien}")]
        public async Task<ActionResult<tbNhanVien>> GetNhanVien(string maNhanVien)
        {
            var nhanVien = await _nhanVienRepository.GetNhanVienById(maNhanVien);
            if (nhanVien == null)
            {
                return NotFound("Không tìm thấy nhân viên.");
            }

            return Ok(nhanVien);
        }

        // Lấy tất cả nhân viên
        [HttpGet]
        public async Task<ActionResult<IEnumerable<tbNhanVien>>> GetAllNhanVien()
        {
            var nhanViens = await _nhanVienRepository.GetAllNhanVien();
            return Ok(nhanViens);
        }
    }
}
