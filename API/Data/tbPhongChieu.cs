using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class tbPhongChieu
    {
        [Key]
        [StringLength(20)]
        public string MaPhong { get; set; }

        [Required]
        public int TongSoGhe { get; set; }

        public char LoaiMayChieu { get; set; }
        public char LoaiAmThanh { get; set; }
        public decimal? DienTich { get; set; }
        public bool? TinhTrang { get; set; } = true;
    }
}
