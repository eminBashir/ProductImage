namespace ProductImageApi.Data.Entity
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }

    }
}
