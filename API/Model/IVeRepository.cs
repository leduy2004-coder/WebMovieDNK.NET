using API.Data;
using API.Dto;

namespace API.Model
{
    public interface IVeRepository
    {
        Task<tbVe> AddVe(VeDTO v);
        Task<tbVe> UpdateVe(tbVe v);
        Task<bool> DeleteVe(string maVe);
        Task<IEnumerable<tbVe>> GetDanhSachVe();
        Task<tbVe> GetThongTinVe(string maPhim);
    }
}
