using API.Data;

namespace API.Model
{
    public interface IVeRepository
    {
        Task<tbVe> AddVe(tbVe v);
        Task<tbVe> UpdateVe(tbVe v);
        Task<bool> DeleteVe(string maVe);
        Task<IEnumerable<tbVe>> GetDanhSachVe();
    }
}
