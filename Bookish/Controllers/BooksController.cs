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
        private readonly IStockRepo _stockRepo;

        public BooksController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _booksRepo = new BooksRepo();
            _stockRepo = new StockRepo();
        }
        
        [HttpGet("")]
        public IActionResult BooksPage(string sortBy = "title", bool ascending = true)
        {
            var books = _booksRepo.GetBooksAndStockCount(sortBy, ascending);
            return View(new AllBooksCountModel
            {
                books = books,
                sortBy = sortBy,
                ascending = ascending
            });
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
            _stockRepo.DecommissionBookStock(id);
            
            // _booksRepo.Delete(id);
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

        [HttpGet("stock")]
        public IActionResult BookStockPage(int id)
        {
            var stock = _stockRepo.GetAllCopies(id);
            var selectedBook = _booksRepo.GetBooks().First(book => book.id == id);
            
            return View(new AllBookStock {allBookStock = stock, selectedBook = selectedBook});
        }
        
        [HttpPost("stock/delete")]
        public IActionResult DeleteBookStock(int bookId, int id, bool active)
        {
            Console.WriteLine(active);
            _stockRepo.SetActive(id, !active);

            return RedirectToAction("BookStockPage", new { id = bookId});
        }
    }
}