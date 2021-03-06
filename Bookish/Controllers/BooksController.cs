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
        public IActionResult BooksPage(string sortBy = "title", bool ascending = true, bool isSearch = false, string search = "")
		{
			var books = _booksRepo.GetBooksAndStockCount(sortBy, ascending);
			if (isSearch)
			{
				books = books.Where(b => (FuzzySharp.Fuzz.PartialRatio(b.title.ToLower(), search.ToLower()) > 75 ||
											FuzzySharp.Fuzz.PartialRatio(b.primary_author.ToLower(), search.ToLower()) > 75));
			}
			var model = new AllBooksCountModel
			{
				books = books,
				sortBy = sortBy,
				ascending = ascending
			};
			return View(model);
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

            var id = _booksRepo.Insert(book);
            if (id != 0) // If successful, create new stock for that book
            {
                _stockRepo.Insert(new StockModel
                {
                    book_id = id,
                    is_active = true
                });
            }

            return RedirectToAction("BooksPage");
        }

        [HttpGet("stock/create")]
        public IActionResult CreateStockPage(int book_id)
        {
            var books = _booksRepo.GetBooks();
            var book = books.First(book => book.id == book_id);
            ViewData["currentBook"] = book;
            return View();
        }

        [HttpPost("stock/create")]
        public IActionResult CreateStock(CreateStockRequestModel stockRequestModel)
        {
            var stock = new StockModel
            {
                id = stockRequestModel.id,
                book_id = stockRequestModel.book_id,
                description = stockRequestModel.description,
                is_active = stockRequestModel.is_active
            };

            _stockRepo.Insert(stock);
            
            return RedirectToAction("BookStockPage", new { id = stockRequestModel.book_id});
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
            _stockRepo.SetActive(id, !active);

            return RedirectToAction("BookStockPage", new { id = bookId});
        }
    }
}