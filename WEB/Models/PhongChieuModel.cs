using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class PhongChieuModel
    {
        
        [StringLength(20)]
        public string MaPhong { get; set; }

        public int TongSoGhe { get; set; }

        [StringLength(50)]
        public string LoaiMayChieu { get; set; }

        [StringLength(50)]
        public string LoaiAmThanh { get; set; }

        public decimal DienTich { get; set; }

        
        public bool TinhTrang { get; set; }
    }
}
