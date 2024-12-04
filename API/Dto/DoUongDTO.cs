using API.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dto

{
    public class DoUongDTO
    {
        public string MaDoUong { get; set; }

        public string TenDH { get; set; }

        public string Gia { get; set; }

        public int SoLuongToiDa { get; set; } = 0;

        public int SoLuongDaSuDung { get; set; } = 0;
        public string anh { get; set; }

        public int SoLuongDat {  get; set; }
    }
}
