using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class tbTheLoaiPhim
    {
        [Key]
        [StringLength(20)]
        public string MaLPhim { get; set; }

        [Required]
        [StringLength(50)]
        public string TenLPhim { get; set; }

        [StringLength(100)]
        public string MoTaLP { get; set; }
    }
}
