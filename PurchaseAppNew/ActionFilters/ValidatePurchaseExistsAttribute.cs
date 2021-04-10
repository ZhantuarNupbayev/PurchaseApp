using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Repository.Manager;
using Services.Logger;
using System;
using System.Threading.Tasks;
using DataLayer.Entities;
using Microsoft.AspNetCore.Identity;

namespace PurchaseAppNew.ActionFilters
{
    public class ValidatePurchaseExistsAttribute: IAsyncActionFilter
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _manager;

        public ValidatePurchaseExistsAttribute(IRepositoryManager repository, 
            ILoggerManager logger,
            UserManager<User> manager)
        {
            _repository = repository;
            _logger = logger;
            _manager = manager;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context,
            ActionExecutionDelegate next)
        {
            var method = context.HttpContext.Request.Method;
            var trackChanges = (method.Equals("PUT") || method.Equals("PATCH")) ? true : false;
            
            var userId = _manager.FindByNameAsync(context.HttpContext.User.Identity.Name).Result.Id;
            var id = (Guid)context.ActionArguments["id"];          
            var purchase = await _repository.Purchase.GetPurchaseAsync(userId, id, trackChanges);

            if (purchase == null)
            {
                _logger.LogInfo($"Purchase with id: {id} doesn't exist in the database");
                context.Result = new NotFoundResult();
            }
            else
            {
                var product = await _repository.Product.GetProductByIdAsync(purchase.ProductId, false);

                if (product == null)
                {
                    _logger.LogInfo($"Product with id: {purchase.ProductId} doesn't exist in the database.");
                    context.Result = new NotFoundResult();
                    return;
                }
                
                context.HttpContext.Items.Add("purchase", purchase);
                await next();
            }
        }
    }
}
