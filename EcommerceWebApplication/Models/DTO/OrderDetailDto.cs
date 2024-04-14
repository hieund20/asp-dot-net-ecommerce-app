using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Models.DTO
{
    public class OrderDetailDto
    {
        public Guid ProductId { get; set; }
        public Guid OrderId { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public double Discount { get; set; }

        //Navigation propertiesz    
        public Product Product { get; set; }
        public Order Order { get; set; }
    }
}
