using AutoMapper;
using Ecommerce.API.Migrations;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;
using Ecommerce.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ICartItemRepository cartItemRepository;

        public CartItemsController(IMapper mapper, ICartItemRepository cartItemRepository)
        {
            this.mapper = mapper;
            this.cartItemRepository = cartItemRepository;
        }

        [HttpGet]
        [Route("{CartSessionId:Guid}")]
        public async Task<IActionResult> GetCartItems([FromRoute] Guid CartSessionId)
        {
            var cartItemDomainModel = await cartItemRepository.GetCartItemsAsync(CartSessionId);

            return Ok(mapper.Map<List<CartItemDto>>(cartItemDomainModel));
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromQuery] Guid ProductId, [FromQuery] Guid CartSessionId)
        {
            var cartItemDomainModel = await cartItemRepository.AddToCartAsync(ProductId, CartSessionId);

            //Map Domain Model to DTO
            return Ok(mapper.Map<CartItemDto>(cartItemDomainModel));
        }

        [HttpGet]
        [Route("GetCartItemsTotal/{CartSessionId:Guid}")]
        public async Task<IActionResult> GetCartItemsTotal([FromRoute] Guid CartSessionId)
        {
            decimal cartItemDomainModel = await cartItemRepository.GetTotalAsync(CartSessionId);

            return Ok(cartItemDomainModel);
        }
    }
}
