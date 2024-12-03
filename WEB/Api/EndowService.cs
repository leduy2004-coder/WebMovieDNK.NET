using System;
using Web.Api;
using WEB.Models;

namespace WEB.Api
{
    public class EndowService
    {
        private readonly ApiService _apiService;
        public EndowService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<UuDaiModel>> GetUuDais()
        {
            return await _apiService.GetDataAsync<List<UuDaiModel>>("api/UuDai/all");
        }

        public async Task<UuDaiModel> GetUuDaiById(string maUuDai)
        {
         
            return await _apiService.GetDataAsync<UuDaiModel>($"api/UuDai/{maUuDai}");
        }
    }
}
