using API.Data;


namespace API.Model
{
    public interface IPhimRepository
    {
        Task<IEnumerable<tbPhim>> GetPhimDangChieu();
        Task<IEnumerable<tbPhim>> GetPHIMs();
        Task<tbPhim> GetThongTinPhim(string maPhim);
        Task<IEnumerable<tbPhim>> GetPhimChuaChieu(); 
        Task<bool> DeletePHIM(string maPhim);
        Task<tbPhim> AddPHIM(tbPhim phim);

        Task<tbPhim> UpdatePHIM(tbPhim phim);
      
    }
}
