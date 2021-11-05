using System;
using System.Linq;
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
        private readonly IStockRepo _stockRepo;
        
        public UsersController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _usersRepo = new UsersRepo();
            _stockRepo = new StockRepo();
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

        [HttpGet("edit")]
        public IActionResult EditUserPage(int id)
        {
            var users = _usersRepo.GetUsers();
            var user = users.First(user => user.id == id);
            ViewData["currentUser"] = user;
            return View();
        }

        [HttpPost("edit")]
        public IActionResult EditUser(EditUserRequestModel userModel)
        {
            var user = new UserModel
            {
                id = userModel.id,
                first_name = userModel.first_name,
                last_name = userModel.last_name,
                email_address = userModel.email_address,
                balance = userModel.balance
            };

            _usersRepo.Update(user);

            return RedirectToAction("UsersPage");
        }
        
        [HttpGet("loans")]
        public IActionResult LoansPage(int user_id)
        {
            ViewData["loans"] = _stockRepo.GetLoansForUser(user_id);
            return View();
        }
    }
}