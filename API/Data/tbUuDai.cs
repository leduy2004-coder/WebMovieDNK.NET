using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace API.Data
{
    public class tbUuDai
    {
        
            [Key]
            [StringLength(20)]
            public string MaUuDai { get; set; }

            [StringLength(50)]
            public string TieuDe { get; set; }

        public string Anh { get; set; }
        public string NoiDung { get; set; }

            public DateTime? NgayDang { get; set; }
        
    }
}
