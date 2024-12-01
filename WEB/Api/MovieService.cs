
using Web.Models;
using WEB.Models;
namespace Web.Api
{
    public class MovieService
    {
        private readonly ApiService _apiService;
        public MovieService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<MovieModel>> GetPhimListAsync()
        {
            return await _apiService.GetDataAsync<List<MovieModel>>("api/Phim");
        }

        public async Task<List<MovieModel>> GetPhimDangChieu()
        {
            return await _apiService.GetDataAsync<List<MovieModel>>("api/Phim/dang-chieu");
        }

        public async Task<List<MovieModel>> GetPhimSapChieu()
        {
            return await _apiService.GetDataAsync<List<MovieModel>>("api/Phim/sap-chieu");
        }

        public async Task<MovieModel> GetDanhMucAsync(string maPhim)
        {
            string url = $"api/Phim/{maPhim}";
            return await _apiService.GetDataAsync<MovieModel>(url);
        }

        public async Task<MovieModel> GetThongTinPhimAsync(string maPhim)
        {
            string url = $"api/Phim/{maPhim}";
            return await _apiService.GetDataAsync<MovieModel>(url);
        }

        public async Task<List<String>> GetNgayXem(string maPhim)
        {

            string url = $"api/Phim/ngayxem/{maPhim}";
            return await _apiService.GetDataAsync<List<String>>(url);
        }
        // Lấy danh sách suất chiếu của phim vào một ngày cụ thể
        public async Task<List<ShiftModel>> GetSuatChieu(string maPhim, String ngayChieu)
        {

            // Cấu trúc URL cho API lấy suất chiếu
            string url = $"api/SuatChieu/{maPhim}/{ngayChieu}";

            // Gọi API và lấy dữ liệu trả về
            return await _apiService.GetDataAsync<List<ShiftModel>>(url);
        }


    }
}
