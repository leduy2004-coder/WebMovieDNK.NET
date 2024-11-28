using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class tbVe
    {
        [Key]
        [StringLength(20)]
        public string MaVe { get; set; }

        [ForeignKey("Phim")]
        public string MaPhim { get; set; }
        public virtual tbPhim Phim { get; set; }

        [ForeignKey("NhanVien")]
        public string MaNV { get; set; }
        public virtual tbNhanVien NhanVien { get; set; }

        public bool? TinhTrang { get; set; } = true;
        public int SoLuongToiDa { get; set; } = 1;
        public int SoLuongDaBan { get; set; } = 1;
        public float? Tien { get; set; }
    }
}
