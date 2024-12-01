
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
    }
}
