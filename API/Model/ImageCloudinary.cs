using API.Model;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

public class CloudinaryService
{
    private readonly Cloudinary _cloudinary;

    public CloudinaryService(IOptions<CloudinarySettings> options)
    {
        var account = new Account(
            options.Value.CloudName,
            options.Value.ApiKey,
            options.Value.ApiSecret
        );
        _cloudinary = new Cloudinary(account);
    }

    public async Task<string> UploadImageAsync(IFormFile file)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("File cannot be null or empty.");

        using (var stream = file.OpenReadStream())
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Width(500).Height(500).Crop("fill")
            };

            var uploadResult = await _cloudinary.UploadAsync(uploadParams);

            if (uploadResult.StatusCode != System.Net.HttpStatusCode.OK)
                throw new Exception("Image upload failed.");

            return uploadResult.SecureUrl.ToString();
        }
    }

    public string GetImageUrl(string publicId)
    {
        if (string.IsNullOrEmpty(publicId))
            throw new ArgumentException("Public ID cannot be null or empty.");

        return _cloudinary.Api.UrlImgUp.BuildUrl(publicId);
    }
    // Phương thức xóa hình ảnh từ Cloudinary bằng SecureUrl
    public async Task<bool> DeleteImageBySecureUrlAsync(string secureUrl)
    {
        if (string.IsNullOrEmpty(secureUrl))
            throw new ArgumentException("Secure URL cannot be null or empty.");

        // Trích xuất publicId từ SecureUrl
        string publicId = ExtractPublicIdFromSecureUrl(secureUrl);

        if (string.IsNullOrEmpty(publicId))
            throw new ArgumentException("Could not extract publicId from Secure URL.");

        // Xóa hình ảnh bằng publicId
        var deleteParams = new DeletionParams(publicId);
        var deletionResult = await _cloudinary.DestroyAsync(deleteParams);

        if (deletionResult.StatusCode == System.Net.HttpStatusCode.OK)
        {
            return true; 
        }

        return false; 
    }

    // Trích xuất publicId từ Secure URL
    private string ExtractPublicIdFromSecureUrl(string secureUrl)
    {
        // Sử dụng Regex để tìm publicId trong URL
        var regex = new Regex(@"image\/upload\/v\d+\/(.+?)\.(jpg|jpeg|png|gif|bmp|webp|tiff|svg)", RegexOptions.IgnoreCase);
        var match = regex.Match(secureUrl);

        if (match.Success)
        {
            return match.Groups[1].Value;
        }

        return string.Empty; 
    }
}
