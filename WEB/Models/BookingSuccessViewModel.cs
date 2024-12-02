namespace WEB.Models
{
    public class BookingSuccessViewModel
    {
        public MovieModel Phim { get; set; }

        public ScheduleModel SuatChieu { get; set; }
        public BookTicketModel BookVe { get; set; }
        public CustomerModel KhachHang { get; set; }

        public string GheDat { get; set; }
    }
}
