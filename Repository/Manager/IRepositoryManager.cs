using Repository.Categories;
using Repository.Products;
using Repository.Purchases;
using System.Threading.Tasks;

namespace Repository.Manager
{
    public interface IRepositoryManager
    {
        ICategoryRepository Category { get; }
        IProductRepository Product { get; }      
        IPurchaseRepository Purchase { get; }
        Task SaveAsync();
    }
}
