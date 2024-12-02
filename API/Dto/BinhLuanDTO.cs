using API.Data;

namespace API.Dto
{
    public class BinhLuanDTO
    {
        public int MaBinhLuan { get; set; }

        public string MaPhim { get; set; }

        public string maKH { get; set; }
        public tbKhachHang KhachHang { get; set; }

        public DateTime Gio { get; set; }

        public string NoiDung { get; set; }

        public bool TinhTrang { get; set; } = true;
    }
}
