using Microsoft.AspNetCore.Identity;

namespace Ecommerce.UI.Models.DTO
{
    public class CommentDto
    {
        public Guid CommentId { get; set; }
        public string Content { get; set; }
        public DateTime CreatedDate { get; set; }
        public Guid ProductId { get; set; }
        public Guid UserId { get; set; }

        //Navigation propertiesz    
        public ProductDto Product { get; set; }
        public IdentityUser User { get; set; }
    }
}
