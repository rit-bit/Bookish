using Bookish.DatabaseInterfaces;
using Bookish.Models;
using Bookish.Models.Requests;
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
        
        [HttpGet("create")]
        public IActionResult CreateUserPage()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult CreateUser(CreateUserRequestModel userModel)
        {
            var user = new UserModel
            {
                first_name = userModel.first_name,
                last_name = userModel.last_name,
                email_address = userModel.email_address
            };

            _usersRepo.Insert(user);

            return RedirectToAction("UsersPage");
        }
    }
}