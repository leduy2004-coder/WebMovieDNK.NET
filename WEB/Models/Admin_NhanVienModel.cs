using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class Admin_NhanVienModel
    {
        public string MaNV { get; set; }

        public string HoTen { get; set; }

        public string Sdt { get; set; }

        public bool? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }

        public string DiaChi { get; set; }

        public long? Cccd { get; set; }
        public bool? TinhTrang { get; set; } = true;

        public string TenTK { get; set; }

        public string MatKhau { get; set; }
        public string MaQL { get; set; }
        public string MaQuanLi { get; set; }

        public string Sdt_Check { get; set; }
    }
}
