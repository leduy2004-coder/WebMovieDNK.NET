using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class tbBookVe
    {
        [Key]
        [StringLength(20)]
        public string MaBook { get; set; }

        [ForeignKey("KhachHang")]
        public string MaKH { get; set; }
        public virtual tbKhachHang KhachHang { get; set; }

        [ForeignKey("NhanVien")]
        public string MaNV { get; set; }
        public virtual tbNhanVien NhanVien { get; set; }

        [ForeignKey("SuatChieu")]
        public string MaSuat { get; set; }
        public virtual tbSuatChieu SuatChieu { get; set; }

        [ForeignKey("Ve")]
        public string MaVe { get; set; }
        public virtual tbVe Ve { get; set; }

        public float? TongTien { get; set; }
        public int SuDungVoucher { get; set; } = 0;
        public DateTime? NgayMua { get; set; }
        public int SoLuongVeDaDat { get; set; } = 0;
    }
}
