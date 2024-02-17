using Ecommerce.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace Ecommerce.API.Models.DTO
{
    public class CartItemDto
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
