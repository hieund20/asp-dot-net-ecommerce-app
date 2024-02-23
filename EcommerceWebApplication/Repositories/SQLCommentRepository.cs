using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.API.Repositories
{
    public class SQLCommentRepository : ICommentRepository
    {
        private readonly EcommerceDBContext dBContext;

        public SQLCommentRepository(EcommerceDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public async Task<Comment> CreateAsync(Comment comment)
        {
            await dBContext.Comments.AddAsync(comment);
            await dBContext.SaveChangesAsync();
            return comment;
        }

        public async Task<Comment?> DeleteAsync(Guid id)
        {
            var existingComment = await dBContext.Comments.FirstOrDefaultAsync(x => x.CommentId == id);

            if (existingComment == null)
            {
                return null;
            }

            dBContext.Comments.Remove(existingComment);
            await dBContext.SaveChangesAsync();
            return existingComment;
        }

        public async Task<List<Comment>> GetAllAsync()
        {
            var comments = await dBContext.Comments.ToListAsync();
            return comments;
        }

        public async Task<Comment?> GetByIdAsync(Guid id)
        {
            return await dBContext.Comments.FirstOrDefaultAsync(x => x.CommentId == id);
        }

        public async Task<Comment?> UpdateAsync(Guid id, Comment comment)
        {
            var existingComment = await dBContext.Comments.FirstOrDefaultAsync(x => x.CommentId == id);

            if (existingComment == null)
            {
                return null;
            }

            existingComment.Content = comment.Content;

            await dBContext.SaveChangesAsync();

            return existingComment;
        }
    }
}
