using Bookish.DatabaseInterfaces;
using Bookish.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Bookish.Controllers
{
    [Route("books")]
    public class BooksController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBooksRepo _booksRepo;

        public BooksController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _booksRepo = new BooksRepo();
        }
        
        [HttpGet("")]
        public IActionResult BooksPage()
        {
            var books = _booksRepo.GetBooks();
            return View(new BooksModel {books = books});
        }
    }
}