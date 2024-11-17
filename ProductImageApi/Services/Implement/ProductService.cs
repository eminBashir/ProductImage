using AutoMapper;
using ProductImageApi.Data.Entity;
using ProductImageApi.Model.Constant;
using ProductImageApi.Model.DTO;
using ProductImageApi.Repository.Interface;
using ProductImageApi.Services.Interface;

namespace ProductImageApi.Services.Implement
{
    public class ProductService : IProductService
    {
        private readonly IWebHostEnvironment _env;
        private readonly IProductRepo _productRepo;
        private readonly IMapper _mapper;
        private readonly IImageService imageService;

        public ProductService(IProductRepo producRepo, IWebHostEnvironment env, IMapper mapper, IImageService imageService)
        {
            _env = env;
            _productRepo = producRepo;
            _mapper = mapper;
            this.imageService = imageService;
        }
        public async Task Create(ProductCreateDTO productCreateDTO)
        {
            var imagePath = await imageService.SaveImage(productCreateDTO.ProImage, FolderNameConstant.PRODUCT_PHOTOS_FOLDER_NAME);
            var productEntity = new Product
            {
                Name = productCreateDTO.Name,
                Price = productCreateDTO.Price,
                Image = imagePath,
            };
            await _productRepo.Create(productEntity);
            await _productRepo.Save();
        }

        public async Task Delete(int id)
        {
            var productEntity = await _productRepo.GetById(id);
            await _productRepo.Delete(productEntity);

            if (productEntity == null)
            {
                throw new Exception("Product not found");
            }
            if (!string.IsNullOrEmpty(productEntity.Image))
            {
                await imageService.DeleteImage(productEntity.Image);
            }
            await _productRepo.Save();
        }

        public async Task<string> GetImageBase64(int id)
        {
            var productEntity = await _productRepo.GetById(id);
            if (productEntity == null)
            {
                throw new Exception("Product not found");
            }
            var fullPath = Path.Combine(_env.WebRootPath, productEntity.Image);
            return await imageService.GetImageBase64Async(fullPath);
        }

        public async Task<ProductGetDTO> GetById(int id)
        {
            var productEntity = await _productRepo.GetById(id);
            if (productEntity == null) throw new Exception("Product not found");
            await imageService.DeleteImage(productEntity.Image);

            var productDto = _mapper.Map<ProductGetDTO>(productEntity);
            return productDto;
        }

        public async Task Update(int id, ProductUpdateDTO productUpdateDTO)
        {
            var product = await _productRepo.GetById(id);
            if (product == null)
            {
                throw new Exception("Product not found");
            }
            product.Image = await imageService.ImageUpdate(product.Image, productUpdateDTO);
            product.Name = productUpdateDTO.Name;
            product.Price = productUpdateDTO.Price;

            await _productRepo.Update(product);
            await _productRepo.Save();
        }
    }
}
