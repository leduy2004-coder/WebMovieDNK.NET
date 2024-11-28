using API.Data;

namespace API.Model
{
    public interface INhanVienRepository
    {
        Task<tbNhanVien> AddNhanVien(tbNhanVien nv);
        Task<tbNhanVien> UpdateNhanVien(tbNhanVien nv);
        Task<bool> DeleteNhanVien(string maNhanVien);
        Task<IEnumerable<tbNhanVien>> GetAllNhanVien();
        Task<string> GetNhanVien(string maNhanVien);
        Task<tbNhanVien> GetNhanVienById(string maNhanVien);
    }
}
