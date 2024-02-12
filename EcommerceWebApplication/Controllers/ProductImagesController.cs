﻿using AutoMapper;
using Ecommerce.API.Models.Domain;
using Ecommerce.API.Models.DTO;
using Ecommerce.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductImagesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IProductImageRepository productImageRepository;

        public ProductImagesController(IMapper mapper, IProductImageRepository productImageRepository)
        {
            this.mapper = mapper;
            this.productImageRepository = productImageRepository;
        }

        [HttpPost]
        [Route("Upload")]
        public async Task<IActionResult> Upload([FromForm] ProductImageUploadRequestDto request)
        {
            ValidateFileUpload(request);

            if (ModelState.IsValid)
            {
                var productImageDomainModel = new ProductImage
                {
                    File = request.File,
                    FileExtension = Path.GetExtension(request.File.FileName),
                    FileSizeInBytes = request.File.Length,
                    FileName = request.FileName,
                    FileDesciption = request.FileDesciption,
                    ProductId = request.ProductId,
                };

                //User repository to Upload Image
                await productImageRepository.Upload(productImageDomainModel);

                return Ok(productImageDomainModel);
            }

            return BadRequest(ModelState);
        }

        private void ValidateFileUpload(ProductImageUploadRequestDto request)
        {
            var allowedExtension = new string[] { ".jpg", ".jpeg", ".png" };

            if (!allowedExtension.Contains(Path.GetExtension(request.File.FileName)))
            {
                ModelState.AddModelError("file", "Unspported file extension");
            }

            if (request.File.Length > 10485760)
            {
                ModelState.AddModelError("file", "File size more than 10MB, please upload a smaller size file.");
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetProductImageById([FromRoute] Guid id)
        {
            var productImagesDomainModel = await productImageRepository.GetAllByProductIdAsync(id);

            // Map Domain Model to Dto
            return Ok(mapper.Map<List<ProductImageDto>>(productImagesDomainModel));
        }
    }
}