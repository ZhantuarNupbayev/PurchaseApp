using DataLayer.Data;
using DataLayer.Entities;
using DataLayer.RequestFeatures;
using Microsoft.EntityFrameworkCore;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Products
{
    public class ProductRepository:
        RepositoryBase<Product>,
        IProductRepository
    {

        public ProductRepository(PurchaseDbContext purchaseContext)
            : base(purchaseContext) { }

        public async Task<Product> GetProductAsync(Guid categoryId, Guid id, bool trackChanges) =>
            await FindWithInclude(p => p.CategoryId.Equals(categoryId) && p.Id.Equals(id), trackChanges, p=>p.Category)
            .SingleOrDefaultAsync();

        public async Task<IEnumerable<Product>> GetAllProductsAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(p => p.ProductName)
            .ToListAsync();
        

        public async Task<PagedList<Product>> GetProductsAsync(Guid categoryId, ProductParameters productParameters, bool trackChanges)
        {
            var products = await FindWithInclude(p => p.CategoryId.Equals(categoryId), trackChanges, p=>p.Category)
            .OrderBy(p => p.ProductName)
            .ToListAsync();

            return PagedList<Product>.ToPagedList(products, productParameters.PageNumber, productParameters.PageSize);
        }

        public async Task<Product> GetProductByIdAsync(Guid id, bool trackChanges) =>
            await FindWithInclude(p => p.Id.Equals(id), trackChanges, p => p.Category)
            .SingleOrDefaultAsync();

        public void CreateProductForCategory(Guid categoryId, Product product)
        {
            product.CategoryId = categoryId;
            Create(product);
        }

        public void DeleteProduct(Product product) => Delete(product);


    }
}
