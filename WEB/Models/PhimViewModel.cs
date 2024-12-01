using Web.Models;

namespace WEB.Models
{
    internal class PhimViewModel
    {
        public PhimModel Phim { get; set; }
        public List<String> NgayChieu { get; set; }  // Danh sách ngày chiếu
        public Dictionary<string, List<CaChieuModel>> SuatChieuTheoNgay { get; set; }
    }
}