using API.Data;
using KTGiuaKi.Dto;

namespace API.Model
{
    public interface IDatVeRepository
    {
        Task<tbBookGhe> ThemMoiBookGhe(tbBookGhe tbBookGhe);
        Task<tbBookVe> ThemMoiBookVe(tbBookVe tbBookVe);
        Task<DatVeThanhCongDTO> LayThongTinDat(string maBook);

    }
}
