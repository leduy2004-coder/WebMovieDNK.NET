using API.Data;

namespace API.Model
{
    public interface IPhongChieuRepository
    {
        Task<tbPhongChieu> GetPhongChieu();
        Task<IEnumerable<tbPhongChieu>> GetDanhSachPhongChieu();
        Task<tbPhongChieu> AddPhongChieu(tbPhongChieu pc);
        Task<tbPhongChieu> UpdatePhongChieu(tbPhongChieu pc);
        Task<bool> DeletePhongChieu(string maPhong);
    }
}
