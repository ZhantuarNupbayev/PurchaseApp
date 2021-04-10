using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.RequestFeatures;
using DataTransfer.DTO.Products;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PurchaseAppNew.ActionFilters;
using Repository.Manager;
using Services.Logger;

namespace PurchaseAppNew.Controllers
{
    [Route("api/categories/{categoryId}/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public ProductsController(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetProducts"), Authorize]
        public async Task<IActionResult> GetProductsForCategory(Guid categoryId, [FromQuery]ProductParameters productParameters)
        {
            var category = await _repository.Category.GetCategoryAsync(categoryId, trackChanges: false);
            if (category == null)
            {
                _logger.LogInfo($"Category with id: {categoryId} doesn't exist in the database.");
                return NotFound();
            }

            var productsFromDb = await _repository.Product.GetProductsAsync(categoryId, productParameters, trackChanges: false);

            Response.Headers.Add("X-Pagination",
                JsonConvert.SerializeObject(productsFromDb.MetaData));

            var productsDto = _mapper.Map<IEnumerable<ProductDto>>(productsFromDb);

            return Ok(productsDto);
        }

        [HttpGet("{id}", Name = "GetProductForCategory"), Authorize]
        public async Task<IActionResult> GetProductForCategory(Guid categoryId, Guid id)
        {
            var category = await _repository.Category.GetCategoryAsync(categoryId, trackChanges: false);
            if (category == null)
            {
                _logger.LogInfo($"Category with id: {categoryId} doesn't exist in the database.");
                return NotFound();
            }

            var productDb = await _repository.Product.GetProductAsync(categoryId, id, trackChanges: false);
            if (productDb == null)
            {
                _logger.LogInfo($"Product with id: {id} doesn't exist in the database.");
                return NotFound();
            }

            var product = _mapper.Map<ProductDto>(productDb);

            return Ok(product);
        }

        [HttpPost, Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateProductForCategory(Guid categoryId, [FromBody]ProductForCreationDto product)
        {
            var category = await _repository.Category.GetCategoryAsync(categoryId, trackChanges: false);

            if (category == null)
            {
                _logger.LogInfo($"Category with id: {categoryId} doesn't exist in the database.");
                return NotFound();
            }

            var productEntity = _mapper.Map<Product>(product);

            _repository.Product.CreateProductForCategory(categoryId, productEntity);
            await _repository.SaveAsync();

            var productToReturn = _mapper.Map<ProductDto>(productEntity);

            return CreatedAtRoute("GetProductForCategory", new { categoryId, id = productToReturn.Id }, productToReturn);
        }

        [HttpDelete("{id}"), Authorize]
        [ServiceFilter(typeof(ValidateProductForCategoryExistsAttribute))]
        public async Task<IActionResult> DeleteProductForCategory(Guid categoryId, Guid id)
        {
            var productForCategory = HttpContext.Items["product"] as Product;

            _repository.Product.DeleteProduct(productForCategory);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}"), Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateProductForCategoryExistsAttribute))]
        public async Task<IActionResult> UpdateProductForCategory(Guid categoryId, Guid id, [FromBody]ProductForUpdateDto product)
        {
            var productEntity = HttpContext.Items["product"] as Product;

            _mapper.Map(product, productEntity);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPatch("{id}"), Authorize]
        [ServiceFilter(typeof(ValidateProductForCategoryExistsAttribute))]
        public async Task<IActionResult> PartiallyUpdateProductForCategory(Guid categoryId, Guid id, [FromBody] JsonPatchDocument<ProductForUpdateDto> patchDoc)
        {
            if (patchDoc == null)
            {
                _logger.LogError("patchDoc object sent from client is null.");
                return BadRequest("patchDoc object is null");
            }

            var productEntity = HttpContext.Items["product"] as Product;

            var productToPatch = _mapper.Map<ProductForUpdateDto>(productEntity);

            patchDoc.ApplyTo(productToPatch, ModelState);

            TryValidateModel(productToPatch);

            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state for the patch document.");
                return UnprocessableEntity(ModelState);
            }

            _mapper.Map(productToPatch, productEntity);

            await _repository.SaveAsync();

            return NoContent();
        }
    }
}