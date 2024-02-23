using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Repositories
{
    public class SQLCommentRepository : ICommentRepository
    {
        private readonly EcommerceDBContext dBContext;

        public SQLCommentRepository(EcommerceDBContext dBContext)
        {
            this.dBContext = dBContext;
        }

        public Task<Comment> CreateAsync(Comment comment)
        {
            throw new NotImplementedException();
        }

        public Task<Comment?> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Comment>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Comment?> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Comment?> UpdateAsync(Guid id, Comment comment)
        {
            throw new NotImplementedException();
        }
    }
}
