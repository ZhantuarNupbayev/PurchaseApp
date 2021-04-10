using DataLayer.Entities;
using DataLayer.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Purchases
{
    public interface IPurchaseRepository
    {
        Task<List<Purchase>> GetPurchasesAsync(string userId, PurchaseParameters purchaseParameters, bool trackChanges);
        Task<Purchase> GetPurchaseAsync(string userId, Guid id, bool trackChanges);
        Task<Purchase> GetPurchaseByProductAsync(Guid productId, string userId, Guid id, bool trackChanges);
        Task<Purchase> GetPurchaseById(Guid id, bool trackChanges);
        void CreatePurchaseForUser(string userId, Guid productId, double price, Purchase purchase);
        void DeletePurchase(Purchase purchase);
    }
}
