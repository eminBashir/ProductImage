namespace ProductImageApi.Model.DTO
{
    public class ProductUpdateDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
         
        public IFormFile NewImage { get; set; }
        public bool ImagesDeleted { get; set; }
    }
}
