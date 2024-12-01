namespace WEB.Models
{
    public class BookTicketViewModel
    {
        public string NgayChieu { get; set; }
        public string ThoiGian { get; set; }
        public MovieModel Movie { get; set; }
        public ScheduleModel Sche { get; set; }
        public List<ChairModel> ListChair { get; set; }
        public List<ChairModel> ListChairBook { get; set; }
        public decimal Money { get; set; }
    }
}
