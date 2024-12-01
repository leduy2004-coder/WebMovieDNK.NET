using Microsoft.AspNetCore.Mvc;
using System;
using Web.Api;
using WEB.Models;

namespace WEB.Controllers
{
    [Route("BookTicket")]
    public class BookTicketController : Controller
    {
        private readonly ScheduleService _schedule;

        public BookTicketController(ScheduleService schedule)
        {
            _schedule = schedule;
        }

        // Trang hiển thị thông tin đặt vé
        [HttpGet("GetBookTicket")]
        public async Task<IActionResult> GetBookTicket(string maSuat)
        {
            var schedule = await _schedule.GetScheduleById(maSuat);
            if (schedule == null)
            {
                return NotFound();
            }

            var listChair = await _schedule.GetChairListAsync();
            var listChairBook = await _schedule.GetChairBooked(maSuat);

            var model = new BookTicketViewModel
            {
                Sche = schedule,
                NgayChieu = schedule.NgayChieu.ToString("dd/MM/yyyy"),
                ThoiGian = schedule.CaChieu.ThoiGianBatDau.ToString(@"hh\:mm"),
                Movie = schedule.phim,
                Money = 60000,
                ListChair = listChair,
                ListChairBook = listChairBook,
            };

            return View("BookTicket", model);
        }

        // Xử lý khi người dùng nhấn "Đặt vé"
        [HttpPost("Book")]
        public async Task<IActionResult> Book([FromForm] BookTicketRequest request)
        {
            if (string.IsNullOrEmpty(request.chairBook))
            {
                ModelState.AddModelError(string.Empty, "Vui lòng chọn ghế trước khi đặt vé.");
                return BadRequest(ModelState);
            }

            // Lấy thông tin lịch chiếu
            var schedule = await _schedule.GetScheduleById(request.maSC);
            if (schedule == null)
            {
                return NotFound("Không tìm thấy lịch chiếu.");
            }
            var maKH = HttpContext.Session.GetString("UserId");
            // Xử lý đặt vé
            var isBooked = await _schedule.BookTicketsAsync(request, maKH);
            if (!isBooked)
            {
                return BadRequest("Đặt vé không thành công.");
            }

            // Thành công, chuyển hướng đến trang xác nhận
            return RedirectToAction("Confirm", new { ticket = request });
        }

        // Trang xác nhận đặt vé
        [HttpGet("Confirm")]
        public IActionResult Confirm(BookTicketRequest ticket)
        {
            ViewBag.Message = "Đặt vé thành công!";
            
            return View("Confirm", ticket);
        }
    }

    public class BookTicketRequest
    {
        public string maPhim { get; set; }
        public string chairBook { get; set; } 
        public int totalMoney { get; set; }  
        public string maSC { get; set; }   
    }
}
