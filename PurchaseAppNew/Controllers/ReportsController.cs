using System.Threading.Tasks;
using DataLayer.Entities;
using DataLayer.RequestFeatures;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Logger;
using Services.Report;

namespace PurchaseAppNew.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : Controller
    {
        private readonly IReportManager _reportManager;
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;

        public ReportsController(IReportManager reportManager,
            ILoggerManager logger,
            UserManager<User> userManager)
        {
            _reportManager = reportManager;
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet, Authorize]
        public async Task<IActionResult> Get([FromQuery] PurchaseParameters purchaseParameters)
        {
            var userId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;

            var purchasesbyCategory = await _reportManager.GetPurchasesByCategory(userId, purchaseParameters);

            return Ok(purchasesbyCategory);
        }

        [HttpGet("max"), Authorize]
        public async Task<IActionResult> GetMaximum([FromQuery] PurchaseParameters purchaseParameters)
        {
            var userId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;

            var maximumPurchases = await _reportManager.GetMaximumPurchase(userId, purchaseParameters);

            return Ok(maximumPurchases);
        }

        [HttpGet("min"), Authorize]
        public async Task<IActionResult> GetMinimum([FromQuery] PurchaseParameters purchaseParameters)
        {
            var userId = _userManager.FindByNameAsync(User.Identity.Name).Result.Id;

            var minimumPurchases = await _reportManager.GetMinimumPurchase(userId, purchaseParameters);

            return Ok(minimumPurchases);
        }

    }
}
