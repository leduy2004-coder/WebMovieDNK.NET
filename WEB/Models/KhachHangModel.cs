using System;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class KhachHangModel
    {
        internal bool IsSuccessStatusCode;

        
      
        public string MaKH { get; set; }

       
        public string HoTen { get; set; }

       
        public string Sdt { get; set; }
       
        public DateTime? NgaySinh { get; set; }

       
        public string Email { get; set; }

        public bool? TinhTrang { get; set; } = true;

        
        public string TenTK { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

        public int DiemThuong { get; set; } = 0;
        public int SoLuongVoucher { get; set; } = 0;
        public int DiemDanh { get; set; } = 0;

        public DateTime? NgayDiemDanhCuoi { get; set; }

       
    }
}
