using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class DrinkModel
    {
        public string MaDoUong { get; set; }

        public string TenDH { get; set; }


        public string Gia { get; set; }

  
        public int SoLuongToiDa { get; set; } = 0;


        public int SoLuongDaSuDung { get; set; } = 0;
        public string anh { get; set; }

    }
}
