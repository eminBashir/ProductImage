using ProductImageApi.Model.Constant;
using ProductImageApi.Model.DTO;
using ProductImageApi.Services.Interface;

namespace ProductImageApi.Services.Implement
{
    public class ImageService : IImageService
    {
        private readonly IWebHostEnvironment environment;

        public ImageService(IWebHostEnvironment environment)
        {
            this.environment = environment;
        }

        public async Task<string> SaveImage(IFormFile image, string folderName)
        {
            var fileName = string.Concat(Guid.NewGuid().ToString(), image.FileName);
            var folderPath = Path.Combine(environment.WebRootPath, folderName);
            var fullPath = Path.Combine(folderPath, fileName);

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            using (var stream = new FileStream(fullPath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            return fullPath;
        }

        public async Task DeleteImage(string filePath)
        {
            var fullPath = Path.Combine(environment.WebRootPath, filePath);

            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }

        }

        public async Task<string> GetImageBase64Async(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found");
            }
            byte[] imageByte = await File.ReadAllBytesAsync(filePath);
            return Convert.ToBase64String(imageByte);
        }

        public async Task<string> ImageUpdate(string currentImage, ProductUpdateDTO productUpdateDTO)
        {
            if (productUpdateDTO.ImagesDeleted && !string.IsNullOrEmpty(currentImage))
            {
                var fullPath = Path.Combine(environment.WebRootPath, currentImage);
                await DeleteImage(fullPath);
                return null;
            }
            else if (productUpdateDTO.NewImage != null)
            {
                if (!string.IsNullOrEmpty(currentImage))
                {
                    var fullPath = Path.Combine(environment.WebRootPath, currentImage);
                    await DeleteImage(fullPath);
                }
                return await SaveImage(productUpdateDTO.NewImage, FolderNameConstant.PRODUCT_PHOTOS_FOLDER_NAME);
            }
            return currentImage;
        }
    }
}
