using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    public class KhachHangUuDai
    {
        [Key]
        [Column(Order = 0)]
        [StringLength(20)]
        public string MaKH { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(20)]
        public string MaUuDai { get; set; }

        
    }
}
