using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class tbBookVe
    {
        [Key]
        [StringLength(20)]
        public string MaBook { get; set; }

        public string? MaKH { get; set; }

        public string? MaNV { get; set; }


        public string MaSuat { get; set; }


        public string MaVe { get; set; }

        public double? TongTien { get; set; }
        public int SuDungVoucher { get; set; } = 0;
        public DateTime? NgayMua { get; set; }
        public int SoLuongVeDaDat { get; set; } = 0;
    }
}
