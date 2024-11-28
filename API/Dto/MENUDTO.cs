using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KTGiuaKi.Dto
{
    public class MENUDTO
    {
        public int? IDMON { get; set; }


        public string TENMON { get; set; }


        public string DONVITINH { get; set; }

        public decimal DONGIA { get; set; }


        public decimal SOLUONG { get; set; }

  
        public string MOTA { get; set; }

  
        public string HINHANH { get; set; }

        public int IDDANHMUC { get; set; }

  
        public DANHMUCDTO DANHMUC { get; set; } 
    }
}
