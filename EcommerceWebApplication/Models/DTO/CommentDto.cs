using Ecommerce.API.Models.Domain;
using Microsoft.AspNetCore.Identity;

namespace Ecommerce.API.Models.DTO
{
    public class CommentDto
    {
        public Guid CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

        //Navigation propertiesz    
        public Product Product { get; set; }
        public IdentityUser User { get; set; }
    }
}
