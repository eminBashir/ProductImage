namespace ProductImageApi.Model.DTO
{
    public class ProductCreateDTO
    {
       
        public string Name { get; set; }
        public decimal Price { get; set; }
        public IFormFile ProImage { get; set; }
        
    }
}
