using API.Data;

namespace API.Dto

{
    public class SuatChieuDTO
    {
      
        public string MaSuat { get; set; }

        public string MaPhim { get; set; }
        //public virtual DatVeThanhCongDTO Phim { get; set; }

        public virtual tbCaChieu CaChieu { get; set; }

        public string MaPhong { get; set; }

        public string MaCa { get; set; }
     
        public DateTime NgayChieu { get; set; }

  
        public bool TinhTrang { get; set; }
    }
}
