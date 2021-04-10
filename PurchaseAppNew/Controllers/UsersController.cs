using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Services.Logger;
using System.Threading.Tasks;

namespace PurchaseAppNew.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ILoggerManager _logger;
        private readonly UserManager<User> _userManager;

       public UsersController(ILoggerManager logger,
           UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        [HttpGet("{userId}"), Authorize]
        public async Task<IActionResult> GetUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            _logger.LogInfo($"{user.UserName} is connected");

            return Ok(user.UserName);
        }
    }
}
