using API.Data;
using API.Dto;
using API.Model;
using KTGiuaKi.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YourApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Admin_QLSuatChieuController : ControllerBase
    {
        private readonly ISuatChieuRepository _suatChieuService;

        // Constructor injection để inject service vào controller
        public Admin_QLSuatChieuController(ISuatChieuRepository suatChieuService)
        {
            _suatChieuService = suatChieuService;
        }

        // Phương thức GET để lấy tất cả các suất chiếu chưa chiếu theo mã phim và có ngày chiếu lớn hơn ngày hiện tại
        [HttpGet("GetSuatChuaChieu/{maPhim}")]
        public async Task<IActionResult> GetSuatChuaChieu(string maPhim)
        {
            // Kiểm tra xem mã phim có hợp lệ không
            if (string.IsNullOrEmpty(maPhim))
            {
                return BadRequest("Mã phim không hợp lệ.");
            }

            try
            {
                // Gọi service để lấy danh sách suất chiếu chưa chiếu và có ngày chiếu lớn hơn ngày hiện tại
                var suatChieuList = await _suatChieuService.GetSuatChuaChieu(maPhim);

                // Nếu không có dữ liệu
                if (suatChieuList == null || !suatChieuList.Any())
                {
                    return NotFound("Không tìm thấy suất chiếu chưa chiếu nào cho mã phim này.");
                }

                // Trả về danh sách suất chiếu
                return Ok(suatChieuList);
            }
            catch (Exception ex)
            {
                // Trả về lỗi nếu có lỗi xảy ra
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }


        [HttpGet]
        public async Task<ActionResult<IEnumerable<tbSuatChieu>>> GetAllSuatChieu()
        {
            var suatChieus = await _suatChieuService.GetAllSuatChieu();
            return Ok(suatChieus);
        }

        [HttpDelete("{maSuatChieu}")]
        public async Task<ActionResult> DeleteVe(string maVe)
        {
            var result = await _suatChieuService.DeleteSuatChieu(maVe);
            if (!result)
            {
                return NotFound("Không tìm thấy suất chiếu.");
            }

            return NoContent(); // Trả về 204 No Content nếu xóa thành công
        }

        //// Thêm mới suat chieu
        //[HttpPost]
        //public async Task<ActionResult<tbSuatChieu>> CreateSuatChieu(tbSuatChieu nv)
        //{
        //    if (nv == null)
        //    {
        //        return BadRequest("Thông tin suat chieu không hợp lệ.");
        //    }

        //    // Không cần yêu cầu MaNhanVien, cột này sẽ được tự động sinh
        //    var createdNhanVien = await _suatChieuService.addSuatChieu(nv);

        //    return CreatedAtAction(nameof(GetAllSuatChieu), new { maNhanVien = createdNhanVien.MaSuat }, createdNhanVien);
        //}

        //// Cập nhật thông tin suat chieu
        //[HttpPut("{maSuatChieu}")]
        //public async Task<ActionResult<tbSuatChieu>> UpdateSuatChieu(string maNhanVien, tbSuatChieu nv)
        //{
        //    if (nv == null)
        //    {
        //        return BadRequest("Dữ liệu không hợp lệ.");
        //    }

        //    if (nv.MaSuat != maNhanVien)
        //    {
        //        return BadRequest("Mã suat chieu không khớp.");
        //    }

        //    var updatedNhanVien = await _suatChieuService.upadteSuatChieu(nv);
        //    if (updatedNhanVien == null)
        //    {
        //        return NotFound("Không tìm thấy.");
        //    }

        //    return Ok(updatedNhanVien);
        //}

        //// Xóa suat chieu
        //[HttpDelete("{maSuatChieu}")]
        //public async Task<ActionResult> DeleteSuatChieu(string maNhanVien)
        //{
        //    var result = await _suatChieuService.DeleteSuatChieu(maNhanVien);
        //    if (!result)
        //    {
        //        return NotFound("Không tìm thấy.");
        //    }

        //    return NoContent(); // Trả về 204 No Content nếu xóa thành công
        //}
    }
}

// Thêm mới suat chieu
//[HttpPost]
//public async Task<IActionResult> Create(SuatChieuDTO sc)
//{
//    if (sc == null)
//    {
//        return BadRequest(new { Status = 0, Message = "Thông tin suat chieu không hợp lệ." });
//    }

//    try
//    {
//        var createSuatChieu = await _suatChieuService.AddSuatChieu(sc);

//        if (createSuatChieu != null)
//        {
//            return Ok(new { Status = 1, Message = "Tạo suat chieu thành công.", Data = createSuatChieu });
//        }

//        return BadRequest(new { Status = 0, Message = "Tạo suat chieu thất bại. Vui lòng thử lại." });
//    }
//    catch (Exception ex)
//    {
//        return StatusCode(500, new { Status = -1, Message = $"Đã xảy ra lỗi: {ex.Message}" });
//    }
//}



// Cập nhật thông tin suất chiếu
//[HttpPut("{maSuatChieu}")]
//public async Task<IActionResult> UpdateSuatChieu(string maSuatChieu, tbSuatChieu sc)
//{
//    if (sc == null)
//    {
//        return BadRequest(new { Status = 0, Message = "Dữ liệu suất chiếu không hợp lệ." });
//    }

//    if (sc.MaSuat != maSuatChieu)
//    {
//        return BadRequest(new { Status = 0, Message = "Mã suất chiếu không khớp." });
//    }

//    try
//    {
//        var updatedSuatChieu = await _suatChieuService.UpdateSuatChieu(sc);

//        if (updatedSuatChieu == null)
//        {
//            return NotFound(new { Status = 0, Message = "Không tìm thấy suất chiếu." });
//        }

//        return Ok(new { Status = 1, Message = "Cập nhật suất chiếu thành công.", Data = updatedSuatChieu });
//    }
//    catch (Exception ex)
//    {
//        return StatusCode(500, new { Status = -1, Message = $"Đã xảy ra lỗi: {ex.Message}" });
//    }
//}

//// Xóa suất chiếu
//[HttpDelete("{maSuatChieu}")]
//public async Task<IActionResult> DeleteSuatChieu(string maSuatChieu)
//{
//    try
//    {
//        var result = await _suatChieuService.DeleteSuatChieu(maSuatChieu);

//        if (!result)
//        {
//            return NotFound(new { Status = 0, Message = "Không tìm thấy suất chiếu." });
//        }

//        return Ok(new { Status = 1, Message = "Xóa suất chiếu thành công." });
//    }
//    catch (Exception ex)
//    {
//        return StatusCode(500, new { Status = -1, Message = $"Đã xảy ra lỗi: {ex.Message}" });
//    }
//}


