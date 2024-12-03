using Microsoft.EntityFrameworkCore;
using Web.Api;
using WEB.Models;

namespace WEB.Api
{
    public class TicketService
    {
        private readonly ApiService _apiService;
        public TicketService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<VeModel> GetVeByIdPhim(string maPhim)
        {
            return await _apiService.GetDataAsync<VeModel>($"api/Ve/{maPhim}");

        }

    }
}
