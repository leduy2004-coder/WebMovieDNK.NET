using API.Data;

namespace API.Model
{
    public interface IDatVeRepository
    {
        Task<tbBookGhe> ThemMoiBookGhe(tbBookGhe tbBookGhe);
        Task<tbBookVe> ThemMoiBookVe(tbBookVe tbBookVe);
        
    }
}
