namespace WEB.Models
{
    public class BookingSuccessViewModel
    {
        public MovieModel Phim { get; set; }

        public ScheduleModel SuatChieu { get; set; }
        public BookTicketModel BookVe { get; set; }
        public CustomerModel KhachHang { get; set; }

        public string GheDat { get; set; }

        public List<DrinkDTO> Drinks { get; set; }
    }

    public class DrinkDTO
    {
        public string MaDoUong { get; set; }

        public string TenDH { get; set; }


        public string Gia { get; set; }


        public int SoLuongToiDa { get; set; } = 0;


        public int SoLuongDaSuDung { get; set; } = 0;
        public string anh { get; set; }

        public int SoLuongDat { get; set; }
    }
}
