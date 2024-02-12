using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Repositories
{
    public interface IUserRepository
    {
        Task<IdentityUser> GetByJwtToken(string jwtToken);
        Task<List<string>> GetRoles(string jwtToken);
    }
}
