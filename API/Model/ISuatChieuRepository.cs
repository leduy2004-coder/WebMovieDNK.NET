using API.Data;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Model
{
    public interface ISuatChieuRepository
    {
 
        Task<IEnumerable<tbSuatChieu>> GetSuatChuaChieu(string maPhim);
        Task<IEnumerable<tbSuatChieu>> GetSuatChieuTheoPhim(string maPhim);
        
    }
}
