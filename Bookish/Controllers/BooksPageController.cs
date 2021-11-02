using Bookish.DatabaseInterfaces;
using Bookish.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookish.Controllers
{
    [Route("books")]
    public class BooksPageController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IBooksRepo BooksRepo;

        public BooksPageController(ILogger<HomeController> logger)
        {
            _logger = logger;
            BooksRepo = new BooksRepo();
        }
        
        [HttpGet("")]
        public IActionResult BooksPage()
        {
            var books = BooksRepo.GetBooks();
            return View(new BooksModel {books = books});
        }
    }
}