using ProductImageApi.Controllers;
using ProductImageApi.Data.Entity;

namespace ProductImageApi.Repository.Interface
{
    public interface IProductRepo
    {
        Task Create(Product product);
        Task<Product> GetById(int id);
        Task<int> Save();
        Task Delete(Product product);
        Task Update(Product product);
        Task<Product> GetProductFromBase64(int id);
    }
}
