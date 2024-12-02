using API.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace API.Dto
{
    public class SuatChieuDTO
    {
      

        public string MaPhim { get; set; }
 

        public string MaPhong { get; set; }
    
        public string MaCa { get; set; }
     
        public DateTime NgayChieu { get; set; }

  
        public bool TinhTrang { get; set; }
    }
}
