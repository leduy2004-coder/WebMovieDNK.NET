using System;
using System.ComponentModel.DataAnnotations;

namespace API.Data
{
    public class tbKhachHang
    {
        [Key]
        [StringLength(20)]
        public string? MaKH { get; set; }

        [StringLength(50)]
        public string HoTen { get; set; }

        [StringLength(10)]
        public string Sdt { get; set; }
       
        public DateTime? NgaySinh { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        public bool? TinhTrang { get; set; } = true;

        [StringLength(30)]
        public string TenTK { get; set; }

        [StringLength(200)]
        public string MatKhau { get; set; }

        public int DiemThuong { get; set; } = 0;
        public int SoLuongVoucher { get; set; } = 0;
        public int DiemDanh { get; set; } = 0;

        public DateTime? NgayDiemDanhCuoi { get; set; }

       
    }
}
