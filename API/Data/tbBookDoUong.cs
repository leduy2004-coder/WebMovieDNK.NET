using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Data
{
    [Table("BookDoUong")]
    public class tbBookDoUong
    {
        [StringLength(20)]
        public string MaDoUong { get; set; } 

        [StringLength(20)]
        public string MaBook { get; set; } 

        [Range(1, int.MaxValue)]
        public int SoLuong { get; set; } = 1;

    }
}
