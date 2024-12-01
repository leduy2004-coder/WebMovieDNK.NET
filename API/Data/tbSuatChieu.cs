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

        
        public string MaPhim { get; set; }

        [ForeignKey("MaPhim")]
        public virtual tbPhim Phim { get; set; }

        
        public string MaCa { get; set; }

        [ForeignKey("MaCa")]
        public virtual tbCaChieu CaChieu { get; set; }

        [ForeignKey("PhongChieu")]
        public string MaPhong { get; set; }

        [Required]
        public DateTime NgayChieu { get; set; }

        [Required]
        public bool TinhTrang { get; set; }
    }
}
