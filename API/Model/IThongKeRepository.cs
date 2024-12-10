using API.Data;
using API.Dto;

namespace API.Model
{
    public interface  IThongKeRepository
    {
        Task<List<int>> GetVeBanTungThang(string nam);
        Task<double> GetTongTienTheoNam(string nam);
        Task<List<TopCustomerDTO>> GetTopCustomersByYear(string nam);
        Task<int> GetTongVeTrongNam(string nam);
        Task<int> GetSoLuongPhimDaChieuTrongNam(string nam);


    }
}
