using Web.Models;

namespace WEB.Models
{
    public class HomeViewModel
    {
        public List<PhimModel> PhimDangChieu { get; set; }
        public List<PhimModel> PhimSapChieu { get; set; }
        public List<DateTime> DanhSachNgay { get; set; }
        public List<CaChieuModel> CaChieu { get; set; }
    }
}
