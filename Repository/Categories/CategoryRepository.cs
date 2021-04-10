using DataLayer.Data;
using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repository.Categories
{
    public class CategoryRepository: 
        RepositoryBase<Category>, 
        ICategoryRepository
    {
        public CategoryRepository(PurchaseDbContext purchaseContext) 
            : base(purchaseContext)
        {

        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges) =>
            await FindAll(trackChanges)
            .OrderBy(c => c.CategoryName)
            .ToListAsync();

        public async Task<Category> GetCategoryAsync(Guid categoryId, bool trackChanges) =>
            await FindByCondition(c => c.Id.Equals(categoryId), trackChanges)
            .SingleOrDefaultAsync();

        public void CreateCategory(Category category, bool trackChanges)
        {
            var foundSameCategory = FindByCondition(x => x.CategoryName.Contains(category.CategoryName), trackChanges).SingleOrDefault();
            if (foundSameCategory == null)
            {
                Create(category);
            }
        }

        public async Task<IEnumerable<Category>> GetByIdsAsync(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(x => ids.Contains(x.Id), trackChanges)
            .ToListAsync();

        public void DeleteCategory(Category category) => Delete(category);

    }
}
