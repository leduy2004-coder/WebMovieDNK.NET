using System.ComponentModel.DataAnnotations.Schema;

namespace WEB.Models
{
    public class BookTicketModel
    {
        public string? MaBook { get; set; }
        public string MaKH { get; set; }
        public string? MaNV { get; set; }
        public string MaSuat { get; set; }
        public string? MaVe { get; set; }
        public float? TongTien { get; set; }
        public int SuDungVoucher { get; set; } = 0;
        public DateTime? NgayMua { get; set; }
        public int SoLuongVeDaDat { get; set; } = 0;
    }

}
