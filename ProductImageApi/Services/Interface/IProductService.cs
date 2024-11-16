using ProductImageApi.Data.Entity;
using ProductImageApi.Model.DTO;

namespace ProductImageApi.Services.Interface
{
    public interface IProductService
    {
        Task Create(ProductCreateDTO productCreateDTO);
        Task<ProductGetDTO> GetById(int id);
        Task Update(int id, ProductUpdateDTO productUpdateDTO);
        Task Delete(int id);
        Task <string> GetImageBase64(int id);
    }
}
