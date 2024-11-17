using ProductImageApi.Data.Entity;
using ProductImageApi.Repository.Interface;

namespace ProductImageApi.Repository.Implement
{
    public class ProductRepo : IProductRepo
    {
        private readonly AppDbContext _context;
        public ProductRepo(AppDbContext context)
        {
            _context = context;
        }
        public async Task Create(Product product)
        {
            _context.Products.Add(product);
        }

        public async Task Delete(Product product)
        {
            _context.Products.Remove(product);
            
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<Product> GetProductFromBase64(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<int> Save()
        {
          return await _context.SaveChangesAsync();
        }

        public async Task Update(Product product)
        {
           _context.Update(product);
            
        }
    }
}
