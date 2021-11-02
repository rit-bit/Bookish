using Bookish.DatabaseInterfaces;
using Bookish.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookish.Controllers
{
    [Route("users")]
    public class UsersController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUsersRepo _usersRepo;
        
        public UsersController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _usersRepo = new UsersRepo();
        }

        [HttpGet("")]
        public IActionResult UsersPage()
        {
            var users = _usersRepo.GetUsers();
            return View(new UsersModel {users = users});
        }
    }
}