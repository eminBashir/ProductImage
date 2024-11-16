using ProductImageApi.Model.DTO;

namespace ProductImageApi.Services.Interface
{
    public interface IImageService
    {
        Task<string> SaveImage(IFormFile image,string folderName);
        Task DeleteImage(string filePath);
        Task<string> GetImageBase64Async(string filePath);
        Task<string> ImageUpdate(string currentImage, ProductUpdateDTO productUpdateDTO);
    }
}
