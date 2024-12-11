using API.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Dto

{
    public class VeDTO
    {

        public string? MaVe { get; set; }

        public string? MaPhim { get; set; }

        public string? MaNV { get; set; }

        public bool? TinhTrang { get; set; } = true;
        public int SoLuongToiDa { get; set; } = 100;
        public int SoLuongDaBan { get; set; } = 0;
        public double? Tien { get; set; } = 60000;
    }
}
