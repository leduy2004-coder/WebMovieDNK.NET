using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Net.Http;
using Web.Api;
using WEB.Models;

namespace WEB.Api
{
    public class Admin_QLPhimService
    {
        private readonly ApiService _apiService;
        public Admin_QLPhimService(ApiService apiService)
        {
            _apiService = apiService;
        }
        public async Task<List<Admin_QLPhimModel>> GetListPhim()
        {
            return await _apiService.GetDataAsync<List<Admin_QLPhimModel>>("api/phim/all");

        }
        public async Task<IEnumerable<SelectListItem>> GetListTLPhim()
        {
            var list = await _apiService.GetDataAsync<IEnumerable<TheLoaiPhim>>("api/phim/all-type");

            // Chuyển đổi thành SelectListItem
            var selectList = list.Select(x => new SelectListItem
            {
                Value = x.MaLPhim, 
                Text = x.TenLPhim 
            }).ToList();

            return selectList;
        }

        public async Task<Admin_QLPhimModel> SaveOrUpdatePhimAsync(Admin_QLPhimModel md, IFormFile hinhDaiDienFile, string apiEndpoint, HttpMethod method)
        {
            var formData = new MultipartFormDataContent();

            // Thêm các trường của Admin_QLPhimModel vào form
            AddModelFieldsToFormData(md, formData);

            // Nếu có hình đại diện, thêm hình ảnh vào form data
            AddImageFileToFormData(hinhDaiDienFile, formData);

            // Gửi yêu cầu HTTP POST hoặc PUT đến API
            if (method == HttpMethod.Post)
                return await _apiService.PostDataFormAsync<Admin_QLPhimModel>(apiEndpoint, formData);
            else if (method == HttpMethod.Put)
                return await _apiService.PutDataFormAsync<Admin_QLPhimModel>(apiEndpoint, formData);

            return null;
        }

        private void AddModelFieldsToFormData(Admin_QLPhimModel md, MultipartFormDataContent formData)
        {
            formData.Add(new StringContent(md.MaPhim ?? string.Empty), "MaPhim");
            formData.Add(new StringContent(md.MaLPhim ?? string.Empty), "MaLPhim");
            formData.Add(new StringContent(md.NgayKhoiChieu?.ToString("yyyy-MM-dd") ?? string.Empty), "NgayKhoiChieu");
            formData.Add(new StringContent(md.TenPhim ?? string.Empty), "TenPhim");
            formData.Add(new StringContent(md.DaoDien ?? string.Empty), "DaoDien");
            formData.Add(new StringContent(md.DoTuoiYeuCau.ToString() ?? string.Empty), "DoTuoiYeuCau");
            formData.Add(new StringContent(md.ThoiLuong.ToString() ?? string.Empty), "ThoiLuong");
            formData.Add(new StringContent(md.MoTa ?? string.Empty), "MoTa");
            formData.Add(new StringContent(md.Video ?? string.Empty), "Video");
            formData.Add(new StringContent(md.TinhTrang.ToString()), "TinhTrang");
            formData.Add(new StringContent(md.HinhDaiDien ?? string.Empty), "HinhDaiDien");
        }

        private void AddImageFileToFormData(IFormFile hinhDaiDienFile, MultipartFormDataContent formData)
        {
            if (hinhDaiDienFile != null && hinhDaiDienFile.Length > 0)
            {
                var fileContent = new StreamContent(hinhDaiDienFile.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(hinhDaiDienFile.ContentType);
                formData.Add(fileContent, "HinhDaiDienFile", hinhDaiDienFile.FileName);
            }
         
        }

        public async Task<Admin_QLPhimModel> GetPhimAsync(string manv)
        {
            return await _apiService.GetDataAsync<Admin_QLPhimModel>("api/phim/phim");
        }

        public async Task<bool> DeletePhimAsync(string maPhim)
        {
            if (string.IsNullOrEmpty(maPhim))
            {
                throw new ArgumentException("Mã khách hàng không hợp lệ.");
            }
            // Xây dựng URL API
            string url = $"api/phim/{maPhim}";
            bool deleteSuccess = await _apiService.DeleteDataAsync(url);

            return deleteSuccess;
        }

        public async Task<List<Admin_QLPhimModel>> SearchPhimListAsync(string name)
        {
            string url = $"api/phim/tim/{name}"; ;

            return await _apiService.GetDataAsync<List<Admin_QLPhimModel>>(url);
        }
    }

}
