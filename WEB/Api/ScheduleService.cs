
using Web.Models;
using WEB.Controllers;
using Microsoft.AspNetCore.Mvc;

using WEB.Models;
using API.Data;
namespace Web.Api
{
    public class ScheduleService
    {
        private readonly ApiService _apiService;
        public ScheduleService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<ScheduleModel> GetScheduleById(string maSC)
        {
            string url = $"api/suatchieu/laysuatchieu/{maSC}";
            try
            {
                var sanPham = await _apiService.GetDataAsync<ScheduleModel>(url);
                return sanPham;
            }
            catch
            {
                return null;
            }
        }
        public async Task<List<ChairModel>> GetChairBooked(string maSC)
        {
            string url = $"api/ghe/laysoghedadat/{maSC}";
            try
            {
                var bookedChairs = await _apiService.GetDataAsync<List<ChairModel>>(url);
                return bookedChairs;
            }
            catch
            {
                return null;
            }
        }

        public async Task<List<ChairModel>> GetChairListAsync()
        {
            return await _apiService.GetDataAsync<List<ChairModel>>("api/ghe/all");
        }

        public async Task<string> BookTicketsAsync(BookTicketRequest request, string maKH)
        {
            if (request == null || string.IsNullOrEmpty(request.maSC) || string.IsNullOrEmpty(request.chairBook))
            {
                throw new ArgumentException("Thông tin không hợp lệ.");
            }
            string url = $"api/ve/{request.maPhim}";

            var ve = await _apiService.GetDataAsync<TicketModel>(url);


            var bookVe = new BookTicketModel
            {
                MaBook = "",  
                MaKH = maKH,  
                MaNV = "",  
                MaVe = ve.MaVe,  
                MaSuat = request.maSC,  
                TongTien = request.totalMoney,
                NgayMua = DateTime.Now,
            };



            // Gọi API DatVe để đặt vé
            var responseBookVe = await _apiService.PostDataAsync<BookTicketModel>("/api/datve/bookve", bookVe);
            if (responseBookVe == null)
            {
                return null;
            }

            // Tách danh sách ghế từ ChairBook
            var chairs = request.chairBook.Trim().Split(' '); 

            foreach (var chair in chairs)
            {
                var bookGhe = new BookChairModel
                {
                    MaBook = responseBookVe.MaBook,
                    MaGhe = chair.Trim()
                };

                var responseBookGhe = await _apiService.PostDataAsync<BookChairModel>("/api/DatVe/bookghe", bookGhe);
                if (responseBookGhe == null)
                {
                    return null;
                }
            }

            return responseBookVe.MaBook;
        }

        public async Task<BookingSuccessViewModel> GetInfoBookAsync(string maBook)
        {
            string url = $"api/DatVe/laythongtin/{maBook}";
            return await _apiService.GetDataAsync<BookingSuccessViewModel>(url);
        }

    }

}
