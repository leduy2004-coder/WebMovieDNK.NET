using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly OrderDrinkService _order;
        private readonly TicketService _ticketService;

        public BookTicketController(ScheduleService schedule, OrderDrinkService order, TicketService ticketService)
        {
            _schedule = schedule;
            _order = order;
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
                TempData["Message"] = "Vui lòng chọn ghế trước khi đặt vé.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("GetBookTicket", new { maSuat = request.maSC });
            }
            if (request.datdoUong)
            {
                HttpContext.Session.SetString("TicketData", JsonConvert.SerializeObject(request));  // Lưu vào Session
                return RedirectToAction("OrderDrink");
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

        // Trang đặt đồ uống
        [HttpGet("Order-drink")]
        public async Task<IActionResult> OrderDrink()
        {
            var ticketJson = HttpContext.Session.GetString("TicketData");
            if (string.IsNullOrEmpty(ticketJson))
            {
                return RedirectToAction("Index", "Home"); 
            }

            var bookTicket = JsonConvert.DeserializeObject<BookTicketRequest>(ticketJson);

            var listDrink = await _order.GetAllDrink();
            ViewBag.Ticket = bookTicket;
            ViewBag.Drink = listDrink;
            return View("OrderDrink");
        }
    }

    public class BookTicketRequest
    {
        public string maPhim { get; set; }
        public string TenPhim { get; set; }
        public string NgayChieu { get; set; }
        public string ThoiGian { get; set; }
        public string chairBook { get; set; } 
        public int totalMoney { get; set; }  
        public string maSC { get; set; }   
        public bool datdoUong { get; set; }

        public List<string> maDoUong { get; set; } 
        public List<int> soLuongDoUong { get; set; } 

    }
}
