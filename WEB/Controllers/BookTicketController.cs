using Microsoft.AspNetCore.Mvc;
using System;
using Web.Api;
using WEB.Api;
using WEB.Models;

namespace WEB.Controllers
{
    [Route("BookTicket")]
    public class BookTicketController : Controller
    {
        private readonly ScheduleService _schedule;
        private readonly TicketService _ticketService;



        public BookTicketController(ScheduleService schedule, TicketService ticketService)
        {
            _schedule = schedule;
            _ticketService = ticketService;
        }

        // Trang hiển thị thông tin đặt vé
        [HttpGet("GetBookTicket")]
        public async Task<IActionResult> GetBookTicket(string maSuat)
        {
            var maKH = HttpContext.Session.GetString("UserId");
            if (maKH == null)
            {
                return RedirectToAction("loginView", "Login");
            }
            var schedule = await _schedule.GetScheduleById(maSuat);
            if (schedule == null)
            {
                return NotFound();
            }

            var listChair = await _schedule.GetChairListAsync();
            var listChairBook = await _schedule.GetChairBooked(maSuat);
            var ve = await _ticketService.GetVeByIdPhim(schedule.MaPhim);

            var model = new BookTicketViewModel
            {
                Sche = schedule,
                NgayChieu = schedule.NgayChieu.ToString("dd/MM/yyyy"),
                ThoiGian = schedule.CaChieu.ThoiGianBatDau.ToString(@"hh\:mm"),
                Movie = schedule.phim,
                Money = (decimal) ve.Tien,
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
            var maBook = await _schedule.BookTicketsAsync(request, maKH);
            if (maBook == null)
            {
                return BadRequest("Đặt vé không thành công.");
            }

            // Thành công, chuyển hướng đến trang xác nhận
            return RedirectToAction("Confirm", new { maBook = maBook});
        }

        // Trang xác nhận đặt vé
        [HttpGet("Confirm")]
        public async Task<IActionResult> Confirm(string maBook)
        {
            var model = await _schedule.GetInfoBookAsync(maBook);
       
            return View("Confirm", model);
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
