using API.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTGiuaKi.Dto
{
    public class DatVeThanhCongDTO
    {
        public tbPhim Phim { get; set; }

        public tbSuatChieu SuatChieu { get; set; }
        public tbBookVe BookVe { get; set; }
        public tbKhachHang KhachHang { get; set; }

        public string GheDat { get; set; }

    }
}
