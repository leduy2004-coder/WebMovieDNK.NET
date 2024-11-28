using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class tbCaChieu
    {
        [Key]
        [StringLength(20)]
        public string MaCa { get; set; }

        [StringLength(20)]
        public string TenCa { get; set; }

        [Required]
        public TimeSpan ThoiGianBatDau { get; set; }

        [Required]
        public TimeSpan ThoiGianKetThuc { get; set; }
    }
}
