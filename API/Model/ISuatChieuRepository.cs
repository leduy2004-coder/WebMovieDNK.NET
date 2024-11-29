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
        Task<IEnumerable<DateTime>> GetNgayChieuTheoPhim(string maPhim);
        Task<IEnumerable<DateTime>> GetCaChieuTheoPhimVaNgay(string maPhim, DateTime ngayChieu);



        Task<tbSuatChieu> addSuatChieu(string maSuat);

        Task<tbSuatChieu> upadteSuatChieu(string maSuat);

        Task<bool> deleteSuatChieu(string maSuat);
    }
}
