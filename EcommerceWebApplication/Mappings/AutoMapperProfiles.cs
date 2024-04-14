using AutoMapper;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;

namespace Ecommerce.API.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            //Category
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<AddCategoryRequestDto, Category>().ReverseMap();
            CreateMap<UpdateCategoryRequestDto, Category>().ReverseMap();

            //Product
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<AddProductRequestDto, Product>().ReverseMap();
            CreateMap<UpdateProductRequestDto, Product>().ReverseMap();

            //ProductImage
            CreateMap<ProductImage, ProductImageDto>().ReverseMap();

            //CartItem
            CreateMap<CartItem, CartItemDto>().ReverseMap();

            //Comment
            CreateMap<Comment, CommentDto>().ReverseMap();
            CreateMap<AddCommentRequestDto, Comment>().ReverseMap();
            CreateMap<UpdateCommentRequestDto, Comment>().ReverseMap();

            //Order
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<AddOrderRequestDto, Order>().ReverseMap();
            CreateMap<UpdateOrderRequestDto, Order>().ReverseMap();
        }
    }
}
