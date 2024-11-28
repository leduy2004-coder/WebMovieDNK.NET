using API.Data;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace API.Model
{
    public interface IKhachHangRepository
    {
        Task<IEnumerable<tbKhachHang>> GetDanhSachKhachHang();
        
    }
}
