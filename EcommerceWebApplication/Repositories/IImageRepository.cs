using Ecommerce.API.Models.Domain;

namespace Ecommerce.API.Repositories
{
    public interface IImageRepository
    {
        Task<Image> Upload(Image image);
    }
}
