using API.Data;

namespace API.Model
{
    public interface IQuanLiRepository
    {
        Task<tbQuanLi> AddQuanLi(tbQuanLi ql);
        Task<tbQuanLi> UpdateQuanLi(tbQuanLi ql);
        Task<bool> DeleteQuanli(string maQuanLy);
        Task<IEnumerable<tbQuanLi>> GetDanhSachQuanLi();
    }
}
