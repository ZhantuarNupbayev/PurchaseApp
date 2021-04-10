using DataLayer.RequestFeatures;
using DataTransfer.DTO.Reports;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services.Report
{
    public interface IReportManager
    {
        Task<IEnumerable<ReportDto>> GetMaximumPurchase(string userId, PurchaseParameters purchaseParameters);
        Task<IEnumerable<ReportDto>> GetPurchasesByCategory(string userId, PurchaseParameters purchaseParameters);
        Task<IEnumerable<ReportDto>> GetMinimumPurchase(string userId, PurchaseParameters purchaseParameters);
    }
}
