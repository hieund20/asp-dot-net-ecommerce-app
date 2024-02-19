using AutoMapper;
using Ecommerce.API.Data;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;
using Ecommerce.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IProductRepository productRepository;

        public ProductsController(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] string? filterOn,
                                                [FromQuery] string? filterQuery,
                                                [FromQuery] string? sort,
                                                [FromQuery] bool? isAscending,
                                                [FromQuery] int pageNumber = 1,
                                                [FromQuery] int pageSize = 1000)
        {
            var productsDomainModel = await productRepository.GetAllAsync(filterOn, filterQuery, sort, isAscending ?? true, pageNumber, pageSize);

            return Ok(mapper.Map<List<ProductDto>>(productsDomainModel));
        }

        [HttpGet]
        [Route("Total")]
        public async Task<IActionResult> GetTotal()
        {
            try
            {
                int totalProduct = await productRepository.GetTotalAsync();
                return Ok(totalProduct);

            } catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }    

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddProductRequestDto addProductRequest)
        {
            // Map DTO to Domain Model
            var productDomainModel = mapper.Map<Product>(addProductRequest);

            await productRepository.CreateAsync(productDomainModel);

            //Map Domain Model to DTO
            return Ok(mapper.Map<ProductDto>(productDomainModel));
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            var productDomainModel = await productRepository.GetByIdAsync(id);

            if (productDomainModel == null)
            {
                return NotFound();
            }

            // Map Domain Model to Dto
            return Ok(mapper.Map<ProductDto>(productDomainModel));
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, UpdateProductRequestDto updateProductRequest)
        {
            //Map Dto to Domain model
            var productdomainModel = mapper.Map<Product>(updateProductRequest);

            productdomainModel = await productRepository.UpdateAsync(id, productdomainModel);

            if (productdomainModel == null)
            {
                return NotFound();
            }

            //Map Domain model to Dto
            return Ok(mapper.Map<ProductDto>(productdomainModel));
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var deleteProductDomainModel = await productRepository.DeleteAsync(id);

            if (deleteProductDomainModel == null)
            {
                return NotFound();
            }

            //Map Domain Model to Dto
            return Ok(mapper.Map<ProductDto>(deleteProductDomainModel));
        }
    }
}
