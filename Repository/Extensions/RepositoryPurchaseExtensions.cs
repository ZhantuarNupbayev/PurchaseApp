using DataLayer.Entities;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Repository.Extensions.Utility;

namespace Repository.Extensions
{
    public static class RepositoryPurchaseExtensions
    {
        public static IQueryable<Purchase> FilterPurchases(this IQueryable<Purchase> purchases, string startDate, string endDate) =>
            purchases.Where(p => (p.BuyDate >= Convert.ToDateTime(startDate) && p.BuyDate <= Convert.ToDateTime(endDate)));

        public static IQueryable<Purchase> Search(this IQueryable<Purchase> purchases, string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return purchases;

            var lowerCaseTerm = searchTerm.Trim().ToLower();

            return purchases.Where(p => p.ProductId.ToString().ToLower().Contains(lowerCaseTerm));
        }

        public static IQueryable<Purchase> Sort(this IQueryable<Purchase> purchases, string orderByQueryString)
        {
            if (string.IsNullOrWhiteSpace(orderByQueryString))
                return purchases.OrderBy(p => p.TotalPrice);

            var orderQuery = OrderQueryBuilder.CreateOrderQuery<Purchase>(orderByQueryString);

            if (string.IsNullOrWhiteSpace(orderQuery))
                return purchases.OrderBy(p => p.TotalPrice);

            return purchases.OrderBy(orderQuery);
        }
    }
}
