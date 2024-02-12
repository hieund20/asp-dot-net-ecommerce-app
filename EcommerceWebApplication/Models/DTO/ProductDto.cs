using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Models.DTO
{
    public class ProductDto
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public Guid CategoryId { get; set; }

        //Navigation propertiesz    
        public Category Category { get; set; }
    }
}
