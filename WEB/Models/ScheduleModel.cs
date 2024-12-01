using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Web.Models;

namespace WEB.Models
{
    public class ScheduleModel
    {
        public string MaSuat { get; set; }

        public string MaPhim { get; set; }
        public virtual MovieModel phim { get; set; }

        public string MaCa { get; set; }
        public virtual ShiftModel CaChieu { get; set; }
        public string MaPhong { get; set; }

        public DateTime NgayChieu { get; set; }

        [Required]
        public bool TinhTrang { get; set; }
    }
}
