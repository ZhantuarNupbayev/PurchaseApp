using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using Services.Logger;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PurchaseAppNew.ActionFilters;
using PurchaseAppNew.ModelBinders;
using Repository.Manager;
using DataTransfer.DTO.Categories;
using Microsoft.AspNetCore.Authorization;

namespace PurchaseAppNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;

        public CategoriesController(IRepositoryManager repository, ILoggerManager logger,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetCategories"), Authorize]
        public async Task<IActionResult> GetCategories()
        {         
           var categories = await _repository.Category.GetAllCategoriesAsync(trackChanges: false);

           var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            _logger.LogInfo($"Getting categories from database");

           return Ok(categoriesDto);           
        }

        [HttpGet("{id}", Name = "CategoryById")]
        public async Task<IActionResult> GetCategory(Guid id)
        {
            var category = await _repository.Category.GetCategoryAsync(id, trackChanges: false);

            if (category == null)
            {
                _logger.LogInfo($"Category with id: {id} doesn't exist in the database.");
                return NotFound();
            }
            else
            {
                var categoryDto = _mapper.Map<CategoryDto>(category);
                return Ok(categoryDto);
            }
        }

        [HttpPost]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        public async Task<IActionResult> CreateCategory([FromBody]CategoryForCreationDto category)
        {
            var categoryEntity = _mapper.Map<Category>(category);

            _repository.Category.CreateCategory(categoryEntity, trackChanges: false);
            await _repository.SaveAsync();

            var categoryToReturn = _mapper.Map<CategoryDto>(categoryEntity);

            return CreatedAtRoute("CategoryById", new { id = categoryToReturn.Id }, categoryToReturn);
        }

        [HttpGet("collection/({ids})", Name = "CategoryCollection")]
        public async Task<IActionResult> GetCategoryCollection([ModelBinder(BinderType = typeof(ArrayModelBinder))]IEnumerable<Guid> ids)
        {
            if (ids == null)
            {
                _logger.LogError("Parameter ids is null.");
                return BadRequest("Parameter ids is null");
            }

            var categoryEntities = await _repository.Category.GetByIdsAsync(ids, trackChanges: false);

            if (ids.Count() != categoryEntities.Count())
            {
                _logger.LogError("Some ids are not valid in a collection");
                return NotFound();
            }

            var categoriesToReturn = _mapper.Map<IEnumerable<CategoryDto>>(categoryEntities);
            return Ok(categoriesToReturn);
        }

        [HttpPost("collection")]
        public async Task<IActionResult> CreateCategoryCollection([FromBody]IEnumerable<CategoryForCreationDto> categoryCollection)
        {
            if (categoryCollection == null)
            {
                _logger.LogError("Category collection sent from client is null.");
                return BadRequest("Category collection is null");
            }

            var categoryEntities = _mapper.Map<IEnumerable<Category>>(categoryCollection);
            foreach (var category in categoryEntities)
            {
                _repository.Category.CreateCategory(category, trackChanges: false);
            }

            await _repository.SaveAsync();

            var categoryCollectionToReturn = _mapper.Map<IEnumerable<CategoryDto>>(categoryEntities);
            var ids = string.Join(",", categoryCollectionToReturn.Select(c => c.Id));

            return CreatedAtRoute("CategoryCollection", new { ids }, categoryCollectionToReturn);
        }

        [HttpDelete("{id}")]
        [ServiceFilter(typeof(ValidateCategoryExistsAttribute))]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var category = HttpContext.Items["category"] as Category;

            _repository.Category.DeleteCategory(category);
            await _repository.SaveAsync();

            return NoContent();
        }

        [HttpPut("{id}")]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidateCategoryExistsAttribute))]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryForUpdateDto category)
        {
            var categoryEntity = HttpContext.Items["category"] as Category;

            _mapper.Map(category, categoryEntity);
            await _repository.SaveAsync();

            return NoContent();
        }
    }
}