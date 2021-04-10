using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DataLayer.Entities;
using DataLayer.RequestFeatures;
using DataTransfer.DTO.Purchases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PurchaseAppNew.ActionFilters;
using Repository.Manager;
using Services.Logger;

namespace PurchaseAppNew.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchasesController : ControllerBase
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _manager;
        private readonly IMapper _mapper;

        public PurchasesController(IRepositoryManager repository,
            ILoggerManager logger,
            UserManager<User> manager,
            IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _manager = manager;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetPurchases"), Authorize]
        public async Task<IActionResult> GetPurchasesForUser([FromQuery] PurchaseParameters purchaseParameters)
        {
            var userId = _manager.FindByNameAsync(User.Identity.Name).Result.Id;

            if (!purchaseParameters.ValidDateRange)
                return BadRequest("End Date can't be less than Start Date.");

            var purchasesFromDb = await _repository.Purchase.GetPurchasesAsync(userId, purchaseParameters, trackChanges: false);

            var purchasesDto = _mapper.Map<IEnumerable<PurchaseDto>>(purchasesFromDb);

            return Ok(purchasesDto);
        }

        [HttpGet("{id}", Name = "GetPurchaseById"), Authorize]
        public async Task<IActionResult> GetPurchaseById(Guid id)
        {
            var userId = _manager.FindByNameAsync(User.Identity.Name).Result.Id;

            var purchaseFromDb = await _repository.Purchase.GetPurchaseAsync(userId, id, trackChanges: false);

            var purchaseDto = _mapper.Map<PurchaseDto>(purchaseFromDb);

            return Ok(purchaseDto);
        }

        [HttpPost, Authorize]
        public async Task<IActionResult> CreatePurchaseForUser([FromBody]PurchaseForCreationDto purchase)
        {
            var product = _repository.Product.GetProductByIdAsync(purchase.ProductId, trackChanges: false);

            var price = product.Result.Price;

            if (product == null)
            {
                _logger.LogInfo($"Product with id: {purchase.ProductId} doesn't exist in the database.");
                return NotFound();
            }

            var userId = _manager.FindByNameAsync(User.Identity.Name).Result.Id;

            var purchaseEntity = _mapper.Map<Purchase>(purchase);

            _repository.Purchase.CreatePurchaseForUser(userId, purchase.ProductId, price, purchaseEntity);
            await _repository.SaveAsync();

            var purchaseDto = _mapper.Map<PurchaseDto>(purchaseEntity);

            return CreatedAtRoute("GetPurchaseById", new { id = purchaseDto.Id }, purchaseDto);

        }
    
        [HttpPut("{id}"), Authorize]
        [ServiceFilter(typeof(ValidationFilterAttribute))]
        [ServiceFilter(typeof(ValidatePurchaseExistsAttribute))]
        public async Task<IActionResult> UpdatePurchase(Guid id, [FromBody]PurchaseForUpdateDto purchase)
        {
            var purchaseEntity = HttpContext.Items["purchase"] as Purchase;

            var product = await _repository.Product.GetProductByIdAsync(purchase.ProductId, trackChanges: false) ;

            purchase.UserId = purchaseEntity.UserId;

            purchase.TotalPrice = purchase.Quantity switch
            {
                1 => product.Price,
                _ => product.Price * purchase.Quantity,
            };

            _mapper.Map(purchase, purchaseEntity);
            await _repository.SaveAsync();
             
            return NoContent();
        }
    }
}