using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository.Categories
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> GetAllCategoriesAsync(bool trackChanges);
        Task<Category> GetCategoryAsync(Guid categoryId, bool trackChanges);
        void CreateCategory(Category category, bool trackChanges);
        Task<IEnumerable<Category>> GetByIdsAsync(IEnumerable<Guid> ods, bool trackChanges);
        void DeleteCategory(Category category);
    }
}
