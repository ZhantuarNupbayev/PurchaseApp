using DataLayer.Data;
using DataLayer.Entities;
using DataLayer.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Purchases
{
    public class PurchaseRepository : RepositoryBase<Purchase>, 
        IPurchaseRepository
    {

        public PurchaseRepository(PurchaseDbContext purchaseContext) 
            : base(purchaseContext)
        {

        }

        public void CreatePurchaseForUser(string userId, Guid productId, double price, Purchase purchase)
        {
            purchase.ProductId = productId;
            purchase.UserId = userId;
            purchase.DateCreated = DateTime.Now;

            purchase.TotalPrice = purchase.Quantity switch
            {
                1 => price,
                _ => price * purchase.Quantity,
            };
            Create(purchase);
        }
 
        public void DeletePurchase(Purchase purchase) => Delete(purchase);


        public async Task<Purchase> GetPurchaseAsync(string userId, Guid id, bool trackChanges) =>
            await FindWithInclude(p => p.UserId.Equals(userId) && p.Id.Equals(id), trackChanges, p => p.Product)
            .SingleOrDefaultAsync();

        public async Task<Purchase> GetPurchaseByProductAsync(Guid productId, string userId, Guid id, bool trackChanges) =>
            await FindByCondition(p => p.UserId.Equals(userId) && p.Id.Equals(id) && p.ProductId.Equals(productId), trackChanges)
            .SingleOrDefaultAsync();

        public async Task<Purchase> GetPurchaseById(Guid id, bool trackchanges) =>
            await FindByCondition(p => p.Id.Equals(id), trackchanges)
            .SingleOrDefaultAsync();


        public async Task<List<Purchase>> GetPurchasesAsync(string userId, PurchaseParameters purchaseParameters, bool trackChanges) =>
             await FindWithInclude(p => p.UserId.Equals(userId), trackChanges, p => p.Product)
               .FilterPurchases(purchaseParameters.StartDate, purchaseParameters.EndDate)
               .Search(purchaseParameters.SearchTerm)
               .OrderBy(p => p.TotalPrice)
               .Sort(purchaseParameters.OrderBy)
               .ToListAsync();

    }
}
