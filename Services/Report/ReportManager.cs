using AutoMapper;
using DataLayer.Entities;
using DataLayer.RequestFeatures;
using DataTransfer.DTO.Categories;
using DataTransfer.DTO.Reports;
using Microsoft.EntityFrameworkCore.Internal;
using Repository.Manager;
using Services.Logger;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Services.Report
{
    public class ReportManager : IReportManager
    {
        private readonly IRepositoryManager _repository;
        private readonly ILoggerManager _logger;
        private readonly IMapper _mapper;
        private readonly int NUMBER_TO_TAKE = 5;

        public ReportManager(IRepositoryManager repository, ILoggerManager logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ReportDto>> GetMaximumPurchase(string userId, PurchaseParameters purchaseParameters)
        {
            var purchasesByCategory = await GetPurchasesByCategory(userId, purchaseParameters);

            _logger.LogInfo("Getting maximum purchases");

            var maximumPurchases = purchasesByCategory
                .OrderByDescending(x => x.TotalPrice)
                .Select(r => new ReportDto
                {
                    Category = r.Category,
                    TotalPrice = r.TotalPrice
                })
                .Take(NUMBER_TO_TAKE);

            return maximumPurchases;
        }

        public async Task<IEnumerable<ReportDto>> GetMinimumPurchase(string userId, PurchaseParameters purchaseParameters)
        {
            var purchasesByCategory = await GetPurchasesByCategory(userId, purchaseParameters);

            _logger.LogInfo("Getting minimum purchases");

            var minimumPurchases = purchasesByCategory
                .OrderBy(x => x.TotalPrice)
                .Select(r => new ReportDto
                {
                    Category = r.Category,
                    TotalPrice = r.TotalPrice
                })
                .Take(NUMBER_TO_TAKE);

            return minimumPurchases;
        }

        public async Task<IEnumerable<ReportDto>> GetPurchasesByCategory(string userId, PurchaseParameters purchaseParameters)
        {
            List<Purchase> purchases = await _repository.Purchase.GetPurchasesAsync(userId, purchaseParameters, trackChanges: false);

            var categories = await _repository.Category.GetAllCategoriesAsync(trackChanges: false);

            var categoriesDto = _mapper.Map<IEnumerable<CategoryDto>>(categories);

            var joinedPurchases = purchases.Join(categoriesDto,
                p => p.Product.CategoryId,
                c => c.Id,
                (purchase, category) => new { purchase.TotalPrice, category });

            var reportResult = joinedPurchases.GroupBy(j => j.category)
                .Select(g => new ReportDto
                {
                    Category = g.Key,
                    TotalPrice = g.Sum(c => c.TotalPrice)
                });

            return reportResult;
        }
    }
}
