using API.Data;
using KTGiuaKi.Dto;

namespace API.Model
{
    public interface IDatDoUongRepository
    {
        Task<tbBookDoUong> DatDoUong(tbBookDoUong tbBookDoUong);
        Task<List<tbDoUong>> LayThongTinDoUong();

    }
}
