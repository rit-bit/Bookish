using System;
using System.Linq;
using Bookish.DatabaseInterfaces;
using Bookish.Models;
using Bookish.Models.Requests;
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

        [HttpGet("create")]
        public IActionResult CreateBookPage()
        {
            return View();
        }

        [HttpPost("create")]
        public IActionResult CreateBook(CreateBookRequestModel bookModel)
        {
            var book = new BookModel
            {
                isbn = bookModel.isbn,
                title = bookModel.title,
                primary_author = bookModel.primary_author,
                additional_authors = bookModel.additional_authors
            };

            _booksRepo.Insert(book);
            
            return RedirectToAction("BooksPage");
        }

        [HttpPost("delete")]
        public IActionResult DeleteBook(int id)
        {
            Console.WriteLine(id);
            _booksRepo.Delete(id);
            return RedirectToAction("BooksPage");  
        }
        
        [HttpGet("edit")]
        public IActionResult EditBookPage(int id)
        {
            var books = _booksRepo.GetBooks();
            var book = books.First(book => book.id == id);
            ViewData["currentBook"] = book;
            return View();
        }
        
        [HttpPost("edit")]
        public IActionResult EditBook(EditBookRequestModel bookModel)
        {
            var book = new BookModel
            {
                id = bookModel.id,
                isbn = bookModel.isbn,
                title = bookModel.title,
                primary_author = bookModel.primary_author,
                additional_authors = bookModel.additional_authors
            };

            _booksRepo.Update(book);
            
            return RedirectToAction("BooksPage");
        }
    }
}