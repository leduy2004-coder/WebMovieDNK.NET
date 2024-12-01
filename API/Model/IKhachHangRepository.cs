using API.Data;
using API.Dto;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Model
{
    public interface IKhachHangRepository
    {
        Task<IEnumerable<tbKhachHang>> GetDanhSachKhachHang();
        Task<tbKhachHang> GetKhachHangById(string maKhachHang);
        Task<tbKhachHang> AddKhachHang(tbKhachHang kh);
        Task<tbKhachHang> UpdateKhachHang(tbKhachHang kh);
        Task<bool> DeleteKhachHang(string maKhachHang);
        Task<tbKhachHang> Login(string email, string password);
        Task<bool> RegisterAsync(tbKhachHang khachHang);
        Task<IEnumerable< LichSuKhachHangDTO>> GetLSKhachHang(string maKH);
    }
}
