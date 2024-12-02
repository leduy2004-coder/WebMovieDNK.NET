using System.ComponentModel.DataAnnotations;

namespace API.Data
{
    public class GetCaChieu
    {
        [Key]
        [StringLength(20)]
        public string MaCa { get; set; }

        [StringLength(20)]
        public string TenCa { get; set; }

        public string? MaSuat { get; set; }
        // Thời gian bắt đầu là kiểu TimeSpan
        public TimeSpan ThoiGianBatDau { get; set; }
    }
}
