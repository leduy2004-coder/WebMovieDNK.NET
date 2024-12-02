using API.Data;

namespace API.Model
{
    public interface IBinhLuanRepository
    {
        Task<tbBinhLuan> GetBinhLuan(string maPhim);
        Task<tbBinhLuan> AddBinhLuan(tbBinhLuan bl);
        Task<bool> DeleteBinhLuan(string maBinhLuan);
        Task<IEnumerable<tbBinhLuan>> GetAllBinhLuan(string maPhim);
    }
}
