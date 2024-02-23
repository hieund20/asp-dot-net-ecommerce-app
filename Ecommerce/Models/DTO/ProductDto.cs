namespace Ecommerce.UI.Models.DTO
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public Guid CategoryId { get; set; }
        public string? ThumbnailImageUrl { get; set; }
        public string? Description { get; set; }
        public ICollection<ProductImageDto>? ProductImages { get; set; }

        //Navigation propertiesz    
        public CategoryDto Category { get; set; }
    }
}
