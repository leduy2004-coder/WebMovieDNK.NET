using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class ShiftModel
    {

        public string MaCa { get; set; }


        public string TenCa { get; set; }

  
        public TimeSpan ThoiGianBatDau { get; set; }

   
        public TimeSpan ThoiGianKetThuc { get; set; }
      

    }
}
