using AutoMapper;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;
using Ecommerce.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IOrderRepository orderRepository;

        public OrderController(IMapper mapper, IOrderRepository orderRepository)
        {
            this.mapper = mapper;
            this.orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn,
                                                [FromQuery] string? filterQuery,
                                                [FromQuery] string? sort,
                                                [FromQuery] bool? isAscending,
                                                [FromQuery] int pageNumber = 1,
                                                [FromQuery] int pageSize = 1000)
        {
            var ordersDomainModel = await orderRepository.GetAllAsync(filterOn, filterQuery, sort, isAscending ?? true, pageNumber, pageSize);

            return Ok(mapper.Map<List<OrderDTO>>(ordersDomainModel));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddOrderRequestDto addOrderRequest)
        {
            // Map DTO to Domain Model
            var orderDomainModel = mapper.Map<Order>(addOrderRequest);

            await orderRepository.CreateAsync(orderDomainModel);

            //Map Domain Model to DTO
            return Ok(mapper.Map<ProductDto>(orderDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var orderDomainModel = await orderRepository.GetByIdAsync(id);

            if (orderDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            return Ok(mapper.Map<OrderDTO>(orderDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateOrderRequestDto updateOrderRequest)
        {
            //Map Dto to Domain model
            var orderDomainModel = mapper.Map<Order>(updateOrderRequest);

            orderDomainModel = await orderRepository.UpdateAsync(id, orderDomainModel);

            if (orderDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain model to Dto
            return Ok(mapper.Map<OrderDTO>(orderDomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteOrderDomainModel = await orderRepository.DeleteAsync(id);

            if (deleteOrderDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to Dto
            return Ok(mapper.Map<OrderDTO>(deleteOrderDomainModel));
        }
    }
}
