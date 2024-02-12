using Microsoft.AspNetCore.Identity;

namespace Ecommerce.API.Repositories
{
    public interface ITokenRepository
    {
        string CreateJWTToken(IdentityUser user, List<string> roles);
    }
}
