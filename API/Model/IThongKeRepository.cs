using API.Data;

namespace API.Model
{
    public interface  IThongKeRepository
    {
        Task<List<tbPhim>> ThongKe();
    }
}
