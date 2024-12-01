namespace API.Dto
{
    public class LichSuKhachHangDTO
    {
        public string MaKH { get; set; }       // Mã khách hàng
        public string MaPhim { get; set; }    // Mã phim
        public string TenPhim { get; set; }   // Tên phim
        public double TongTien { get; set; } // Tổng tiền vé
        public DateTime NgayMua { get; set; } // Ngày mua vé
        public string MaBook { get; set; }    // Mã đặt vé
    }
}
