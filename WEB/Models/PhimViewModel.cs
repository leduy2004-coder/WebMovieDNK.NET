using Web.Models;

namespace WEB.Models
{
    internal class PhimViewModel
    {
        public MovieModel Phim { get; set; }
        public List<String> MaSuat { get; set; }
        public List<String> NgayChieu { get; set; }  // Danh sách ngày chiếu
        public Dictionary<string, List<ShiftModel>> SuatChieuTheoNgay { get; set; }
    }
}