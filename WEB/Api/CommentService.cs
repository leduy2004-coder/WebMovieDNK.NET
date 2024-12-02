
using Web.Models;
using WEB.Models;
namespace Web.Api
{
    public class CommentService
    {
        private readonly ApiService _apiService;
        public CommentService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<List<CommentModel>> GetAllComment(string maPhim)
        {
            string url = $"api/BinhLuan/all/{maPhim}";

            try
            {
                var result = await _apiService.GetDataAsync<List<CommentModel>>(url);
                return result;
            }
            catch (HttpRequestException ex)
            {
                return new List<CommentModel>();  // Trả về danh sách rỗng trong trường hợp lỗi
            }
        }


        public async Task<CommentModel> PostComment(CommentModel comment)
        {
            // Gửi yêu cầu POST đến API đăng nhập
            var response = await _apiService.PostDataAsync<CommentModel>("/api/BinhLuan", comment);

            // Kiểm tra kết quả từ API
            if (response != null)
            {
                return response; 
            }

            return null;
        }
    }
}
