namespace WEB.Models
{
    public class PhimModel
    {
        public string? MaPhim { get; set; }

        public string TenPhim { get; set; }
        public string DaoDien { get; set; }

        public int DoTuoiYeuCau { get; set; }
        public DateTime NgayKhoiChieu { get; set; }

        public int ThoiLuong { get; set; }

        public bool? TinhTrang { get; set; } = true;

        public string HinhDaiDien { get; set; }
        public string Video { get; set; }
        public string MoTa { get; set; }
    }
}
