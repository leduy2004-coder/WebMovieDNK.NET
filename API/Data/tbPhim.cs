using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class tbPhim
    {
        [Key]
        [StringLength(20)]
        public string MaPhim { get; set; }

        [ForeignKey("TheLoaiPhim")]
        public string MaLPhim { get; set; }
        public virtual tbTheLoaiPhim TheLoaiPhim { get; set; }

        [Required]
        [StringLength(50)]
        public string TenPhim { get; set; }

        [Required]
        [StringLength(50)]
        public string DaoDien { get; set; }

        [Required]
        public int DoTuoiYeuCau { get; set; }

        [Required]
        public DateTime NgayKhoiChieu { get; set; }

        [Required]
        public int ThoiLuong { get; set; }

        public bool? TinhTrang { get; set; } = true;

        public string HinhDaiDien { get; set; }
        public string Video { get; set; }
        public string MoTa { get; set; }
    }
}
