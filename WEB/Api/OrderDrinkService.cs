using Web.Api;
using WEB.Api;
using WEB.Models;

namespace WEB.Api
{
    public class OrderDrinkService
    {
        private readonly ApiService _apiService;
      
        public OrderDrinkService(ApiService apiService)
        {
            _apiService = apiService;
          
        }

        public async Task<IEnumerable<DrinkModel>> GetAllDrink()
        {
            string url = $"api/DatDoUong/laythongtindouong";
            return await _apiService.GetDataAsync<IEnumerable<DrinkModel>>(url);
        }

        public async Task<IEnumerable<OrderDrinkModel>> PostOrderDrink(OrderDrinkModel drinkModel)
        {
            string url = $"api/DatDoUong/datdouong";
            return await _apiService.PostDataAsync<IEnumerable<OrderDrinkModel>>(url, drinkModel);

        }
    }
}
