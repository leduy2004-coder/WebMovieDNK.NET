using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    public class tbNhanVien
    {
        [Key]
        [StringLength(20)]
        public string MaNV { get; set; }

        [Required]
        [StringLength(50)]
        public string HoTen { get; set; }

        [Required]
        [StringLength(10)]
        public string Sdt { get; set; }

        public bool? GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }

        [StringLength(50)]
        public string DiaChi { get; set; }

        public long? Cccd { get; set; }
        public bool? TinhTrang { get; set; } = true;

        [Required]
        [StringLength(30)]
        public string TenTK { get; set; }

        [Required]
        [StringLength(200)]
        public string MatKhau { get; set; }

        [ForeignKey("QuanLi")]
        public string MaQL { get; set; }
        public virtual tbQuanLi QuanLi { get; set; }

        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Số điện thoại phải có đúng 10 chữ số.")]
        public string Sdt_Check { get; set; }
    }
}
