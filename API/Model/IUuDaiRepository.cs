using API.Data;

namespace API.Model
{
    public interface IUuDaiRepository
    {
        Task<IEnumerable<tbUuDai>> GetUuDais();
        Task<tbUuDai> GetUuDaiById(string maUuDai);
    }
}
