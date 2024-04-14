using Microsoft.AspNetCore.Identity;

namespace Ecommerce.API.Models.Domain
{
    public class OrderDetail
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
