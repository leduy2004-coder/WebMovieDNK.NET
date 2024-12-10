using API.Data;
using API.Dto;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Model
{
    public interface ISuatChieuRepository
    {
 
        Task<IEnumerable<tbSuatChieu>> GetSuatChuaChieu(string maPhim);
        Task<tbSuatChieu> GetSuatChieuTheoMaSC(string maSC);
        Task<IEnumerable<tbGhe>> GetGheDaDat(string maSC);
        Task<IEnumerable<tbGhe>> GetAllGhe();
        Task<tbCaChieu> GetCaChieuTheoMaCa(string maCa);
        Task<IEnumerable<tbSuatChieu>> GetSuatChieuTheoPhim(string maPhim);

        Task<IEnumerable<DateTime>> GetNgayChieuTheoPhim(string maPhim);
        Task<IEnumerable<GetCaChieu>> GetCaChieuTheoPhimVaNgay(string maPhim, string ngayChieu);


        Task<tbSuatChieu> addSuatChieu(SuatChieuDTO maSuat);

        Task<tbSuatChieu> upadteSuatChieu(SuatChieuDTO maSuat);

        Task<bool> DeleteSuatChieu(string maSuat);
        Task<List<tbSuatChieu>> GetAllSuatChieu();

        Task<IEnumerable<tbCaChieu>> GetAvailableCaChieuForSuatChieu(string maPhim, DateTime ngayChieu);
        Task<IEnumerable<tbPhongChieu>> GetAvailablePhongChieuForSuatChieu(string maCa, DateTime ngayChieu);
    }
}
