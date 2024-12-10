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
        public async Task<ActionResult> DeleteSuatChieu(string maSuatChieu)
        {
            var result = await _suatChieuService.DeleteSuatChieu(maSuatChieu);
            if (!result)
            {
                return NotFound("Không tìm thấy suất chiếu.");
            }

            return NoContent(); // Trả về 204 No Content nếu xóa thành công
        }

        [HttpGet("thongTinSC/{maSuatChieu}")]
        public async Task<ActionResult> GetThongTinSC(string maSuatChieu)
        {
            var result = await _suatChieuService.GetSuatChieuTheoMaSC(maSuatChieu);
            if (result == null)
            {
                return NotFound("Không tìm thấy suất chiếu.");
            }

            return Ok(result); // Trả về 204 No Content nếu xóa thành công
        }

        //Cập nhật thông tin suất chiếu
        [HttpPut("updateSC")]
        public async Task<IActionResult> UpdateSuatChieu(SuatChieuDTO sc)
        {
            var updatedSuatChieu = await _suatChieuService.upadteSuatChieu(sc);
            if (updatedSuatChieu == null)
            {
                return NotFound("Không tìm thấy khách hàng."); // Trả về lỗi 404 nếu không tìm thấy
            }

            return Ok(updatedSuatChieu); // Trả về khách hàng đã cập nhật
        }


        // Thêm mới suat chieu
        [HttpPost]
        public async Task<ActionResult<tbSuatChieu>> CreateSuatChieu(SuatChieuDTO suatChieu)
        {
            if (suatChieu == null)
            {
                return BadRequest("Thông tin suat chieu không hợp lệ.");
            }

            // Không cần yêu cầu MaNhanVien, cột này sẽ được tự động sinh
            var createdSuatChieu = await _suatChieuService.addSuatChieu(suatChieu);

            return Ok(createdSuatChieu);
        }

        // Thêm mới suat chieu
        [HttpGet("caChieuAvailable/{ngayChieu}/{maPhim}")]
        public async Task<ActionResult<tbSuatChieu>> getCaChieuControng(DateTime ngayChieu, string maPhim)
        {


            // Không cần yêu cầu MaNhanVien, cột này sẽ được tự động sinh
            var caChieus = await _suatChieuService.GetAvailableCaChieuForSuatChieu(maPhim, ngayChieu);

            return Ok(caChieus);
        }

        [HttpGet("phongChieuAvailable/{maCa}/{ngaChieu}")]

        public async Task<IActionResult> GetAvailablePhongChieu(string maCa, DateTime ngayChieu)
        {

            {
                var availablePhongChieu = await _suatChieuService.GetAvailablePhongChieuForSuatChieu(maCa, ngayChieu);
                return Ok(availablePhongChieu);
            }

        }

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


