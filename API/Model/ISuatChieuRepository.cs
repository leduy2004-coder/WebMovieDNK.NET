using API.Data;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Model
{
    public interface ISuatChieuRepository
    {
 
        Task<IEnumerable<tbSuatChieu>> GetSuatChuaChieu(string maPhim);
        Task<tbSuatChieu> GetSuatChieuTheoMa(string maSC);
        Task<IEnumerable<tbGhe>> GetGheDaDat(string maSC);
        Task<IEnumerable<tbGhe>> GetAllGhe();
        Task<tbCaChieu> GetCaChieuTheoMaCa(string maCa);
        Task<IEnumerable<tbSuatChieu>> GetSuatChieuTheoPhim(string maPhim);

        Task<IEnumerable<DateTime>> GetNgayChieuTheoPhim(string maPhim);
        Task<IEnumerable<GetCaChieu>> GetCaChieuTheoPhimVaNgay(string maPhim, string ngayChieu);


        Task<tbSuatChieu> addSuatChieu(string maSuat);

        Task<tbSuatChieu> upadteSuatChieu(string maSuat);

        Task<bool> deleteSuatChieu(string maSuat);
    }
}
