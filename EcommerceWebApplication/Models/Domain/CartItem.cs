using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models.Domain
{
    public class CartItem
    {
        [Key]
        public Guid ItemId { get; set; }

        public Guid CartId { get; set; }

        public int Quantity { get; set; }

        public DateTime DateCreated { get; set; }

        public Guid ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
