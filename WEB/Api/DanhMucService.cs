namespace WebGK.Api
{
    public class DanhMucService
    {
        private readonly ApiService _apiService;
        public DanhMucService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<DanhMuc>> GetDanhMucListAsync()
        {
            return await _apiService.GetDataAsync<List<DanhMuc>>("api/DanhMuc");
        }

        public async Task<DanhMuc> GetDanhMucAsync(int madanhmucid)
        {
            string url = $"api/DanhMuc/{madanhmucid}";
            return await _apiService.GetDataAsync<DanhMuc>(url);
        }
    }
}
