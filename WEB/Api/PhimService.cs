
using Web.Models;
using WEB.Models;
namespace Web.Api
{
    public class PhimService
    {
        private readonly ApiService _apiService;
        public PhimService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<PhimModel>> GetPhimListAsync()
        {
            return await _apiService.GetDataAsync<List<PhimModel>>("api/Phim");
        }

        public async Task<List<PhimModel>> GetPhimDangChieu()
        {
            return await _apiService.GetDataAsync<List<PhimModel>>("api/Phim/dang-chieu");
        }

        public async Task<List<PhimModel>> GetPhimSapChieu()
        {
            return await _apiService.GetDataAsync<List<PhimModel>>("api/Phim/sap-chieu");
        }

        public async Task<PhimModel> GetDanhMucAsync(string maPhim)
        {
            string url = $"api/Phim/{maPhim}";
            return await _apiService.GetDataAsync<PhimModel>(url);
        }

        public async Task<PhimModel> GetThongTinPhimAsync(string maPhim)
        {
            string url = $"api/Phim/{maPhim}";
            return await _apiService.GetDataAsync<PhimModel>(url);
        }

        public async Task<List<String>> GetNgayXem(string maPhim)
        {

            string url = $"api/Phim/ngayxem/{maPhim}";
            return await _apiService.GetDataAsync<List<String>>(url);
        }
        // Lấy danh sách suất chiếu của phim vào một ngày cụ thể
        public async Task<List<CaChieuModel>> GetSuatChieu(string maPhim, String ngayChieu)
        {
          
            // Cấu trúc URL cho API lấy suất chiếu
            string url = $"api/SuatChieu/{maPhim}/{ngayChieu}";

            // Gọi API và lấy dữ liệu trả về
            return await _apiService.GetDataAsync<List<CaChieuModel>>(url);
        }


    }
}
