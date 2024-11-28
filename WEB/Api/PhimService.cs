
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

        public async Task<PhimModel> GetDanhMucAsync(string maPhim)
        {
            string url = $"api/Phim/{maPhim}";
            return await _apiService.GetDataAsync<PhimModel>(url);
        }
    }
}
