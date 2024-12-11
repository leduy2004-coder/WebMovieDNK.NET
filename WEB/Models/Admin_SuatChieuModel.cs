using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class Admin_SuatChieuModel
    {
      
        public string MaSuat { get; set; }

        public string MaPhim { get; set; }

        public string MaPhong { get; set; }

   
        public string MaCa { get; set; }


        public DateTime NgayChieu { get; set; }


        public bool TinhTrang { get; set; } = true;

    }
}
