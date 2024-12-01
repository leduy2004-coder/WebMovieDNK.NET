using API.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace KTGiuaKi.Dto
{
    public class SuatChieuDTO
    {
        public string MaSuat { get; set; }

        public virtual PhimDTO Phim { get; set; }

        public virtual tbCaChieu CaChieu { get; set; }

        public string MaPhong { get; set; }

        public DateTime NgayChieu { get; set; }

        public bool TinhTrang { get; set; }
    }
}
