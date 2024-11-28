using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Web.Api
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;

        public ApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // GET method
        public async Task<T> GetDataAsync<T>(string url)
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        // POST method
        public async Task<T> PostDataAsync<T>(string url, object data)
        {
            var response = await _httpClient.PostAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // PUT method
        public async Task<T> PutDataAsync<T>(string url, object data)
        {
            var response = await _httpClient.PutAsJsonAsync(url, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }

        // DELETE method
        public async Task<bool> DeleteDataAsync(string url)
        {
            var response = await _httpClient.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
