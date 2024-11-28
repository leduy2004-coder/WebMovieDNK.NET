using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Data
{
    public class tbGhe
    {
        [Key]
        [StringLength(20)]
        public string MaGhe { get; set; } // Primary Key

        [Required]
        public bool TinhTrang { get; set; } // Not null
    }
}
