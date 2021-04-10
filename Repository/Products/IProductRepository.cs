using DataLayer.Entities;
using DataLayer.RequestFeatures;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Products
{
    public interface IProductRepository
    {
        Task<PagedList<Product>> GetProductsAsync(Guid categoryId, ProductParameters productParameters, bool trackChanges);
        Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges);
        Task<Product> GetProductAsync(Guid categoryId, Guid id, bool trackChanges);
        Task<Product> GetProductByIdAsync(Guid id, bool trackChanges);
        void CreateProductForCategory(Guid categoryId, Product product);
        void DeleteProduct(Product product);
    }
}
