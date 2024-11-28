using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class tbSuatChieu
    {
        [Key]
        [StringLength(20)]
        public string MaSuat { get; set; }

        [ForeignKey("Phim")]
        public string MaPhim { get; set; }
        public virtual tbPhim Phim { get; set; }

        [ForeignKey("PhongChieu")]
        public string MaPhong { get; set; }
        public virtual tbPhongChieu PhongChieu { get; set; }

        [ForeignKey("CaChieu")]
        public string MaCa { get; set; }
        public virtual tbCaChieu CaChieu { get; set; }

        [Required]
        public DateTime NgayChieu { get; set; }

        [Required]
        public bool TinhTrang { get; set; }
    }
}
