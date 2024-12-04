using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WEB.Models
{
    public class OrderDrinkModel
    {
        public string MaDoUong { get; set; }

  
        public string MaBook { get; set; }


        public int SoLuong { get; set; } = 1;

    }
}
