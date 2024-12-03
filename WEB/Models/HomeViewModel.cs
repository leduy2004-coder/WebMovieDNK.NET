using Web.Models;

namespace WEB.Models
{
    public class HomeViewModel
    {
        public List<MovieModel> PhimDangChieu { get; set; }
        public List<MovieModel> PhimSapChieu { get; set; }
        public List<DateTime> DanhSachNgay { get; set; }
        public List<ShiftModel> CaChieu { get; set; }
        public List<UuDaiModel> UuDai { get; set; }
    }
}
