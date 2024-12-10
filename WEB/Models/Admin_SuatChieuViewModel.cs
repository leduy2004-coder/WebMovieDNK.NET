using Web.Models;

namespace WEB.Models
{
    public class Admin_SuatChieuViewModel
    {
        public List<Admin_SuatChieuModel> listSuatChieu { get; set; }
        public List<MovieModel> PhimDangChieu { get; set; }
        public List<DateTime> DanhSachNgay { get; set; }
        public List<ShiftModel> CaChieu { get; set; }
        public List<PhongChieuModel> PhongChieu { get; set; }
    }
}
