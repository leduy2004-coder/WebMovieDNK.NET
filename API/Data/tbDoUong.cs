using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    public class tbDoUong
    {
        [Key]
        [StringLength(20)]
        public string MaDoUong { get; set; } 

        [Required]
        [StringLength(50)]
        public string TenDH { get; set; }

        [StringLength(100)]
        public string Gia { get; set; } 

        [Range(0, int.MaxValue)]
        public int SoLuongToiDa { get; set; } = 0; 

        [Range(0, int.MaxValue)]
        public int SoLuongDaSuDung { get; set; } = 0; 
        public string anh { get; set; }
    }
}
