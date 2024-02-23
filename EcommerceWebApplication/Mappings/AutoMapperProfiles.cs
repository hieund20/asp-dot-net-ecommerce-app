using AutoMapper;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;

namespace Ecommerce.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<AddCategoryRequestDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryRequestDto, Category>().ReverseMap();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<AddProductRequestDto, Product>().ReverseMap();
            CreateMap<UpdateProductRequestDto, Product>().ReverseMap();

            CreateMap<ProductImage, ProductImageDto>().ReverseMap();

            CreateMap<CartItem, CartItemDto>().ReverseMap();

            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<AddCommentRequestDto, Comment>().ReverseMap();
            CreateMap<UpdateCommentRequestDto, Comment>().ReverseMap();
        }
    }
}
