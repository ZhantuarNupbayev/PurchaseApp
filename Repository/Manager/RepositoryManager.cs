using DataLayer.Data;
using Repository.Categories;
using Repository.Products;
using Repository.Purchases;
using System.Threading.Tasks;

namespace Repository.Manager
{
    public class RepositoryManager : IRepositoryManager
    {
        private PurchaseDbContext _purchaseContext;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private IPurchaseRepository _purchaseRepository;

        public RepositoryManager(PurchaseDbContext purchaseContext)
        {
            _purchaseContext = purchaseContext;
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_categoryRepository == null)
                    _categoryRepository = new CategoryRepository(_purchaseContext);

                return _categoryRepository;
            }
        }

        public IProductRepository Product
        {
            get
            {
                if (_productRepository == null)
                    _productRepository = new ProductRepository(_purchaseContext);

                return _productRepository;
            }
        }

        public IPurchaseRepository Purchase
        {
            get
            {
                if (_purchaseRepository == null)
                    _purchaseRepository = new PurchaseRepository(_purchaseContext);

                return _purchaseRepository;
            }
        }

        public Task SaveAsync() => _purchaseContext.SaveChangesAsync();
    }
}
