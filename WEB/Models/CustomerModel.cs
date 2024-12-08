using System;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class CustomerModel
    {
        internal bool IsSuccessStatusCode;

        public string MaKH { get; set; }

       
        public string HoTen { get; set; }

       
        public string Sdt { get; set; }
       
        public DateTime? NgaySinh { get; set; }

       
        public string Email { get; set; }

        public bool? TinhTrang { get; set; } = true;

        public string VerificationCode { get; set; }
        public string TenTK { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string MatKhau { get; set; }

       
    }
}
