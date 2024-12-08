using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Data
{
    public class tbNhanVien
    {
        [Key]
        [StringLength(20)]
        public string? MaNV { get; set; }

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

        public int MaRole { get; set; }
      

        
    }
}
