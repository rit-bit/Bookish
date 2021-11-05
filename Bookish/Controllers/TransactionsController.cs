using System;
using Bookish.DatabaseInterfaces.Transactions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookish.Controllers
{
    [Route("transactions")]
    public class TransactionsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ITransactionsRepo _transactionsRepo;

        public TransactionsController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _transactionsRepo = new TransactionsRepo();
        }

        [HttpGet("check_out")]
        public IActionResult CheckOutStockPage()
        {
            ViewData["user_id"] = 0;
            return View();
        }
        
        [HttpPost("check_out")]
        public IActionResult CheckOutStock(int user_id, int stock_id)
        {
            _transactionsRepo.CheckIn(stock_id); // If this fails it should do so silently
            _transactionsRepo.CheckOut(stock_id, user_id, null);
            ViewData["user_id"] = user_id;
            return RedirectToAction("CheckOutStockPage");
        }
        
        [HttpGet("check_in")]
        public IActionResult CheckInStockPage()
        {
            return View();
        }
        
        [HttpPost("check_in")]
        public IActionResult CheckInStock(int stock_id)
        {
            _transactionsRepo.CheckIn(stock_id);
            return RedirectToAction("CheckInStockPage");
        }
    }
}