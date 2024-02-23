using Microsoft.AspNetCore.Identity;

namespace Ecommerce.API.Models.Domain
{
    public class Comment
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
