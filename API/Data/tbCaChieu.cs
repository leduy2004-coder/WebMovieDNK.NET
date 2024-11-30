using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Data
{
    public class tbCaChieu
    {
        [Key]
        [StringLength(20)]
        public string MaCa { get; set; }

        [StringLength(20)]
        public string TenCa { get; set; }

        [StringLength(20)]
        public string MaSuat { get; set; }

        // Thời gian bắt đầu là kiểu TimeSpan
        public TimeSpan ThoiGianBatDau { get; set; }
    }
}
