﻿using System.Collections.Generic;
using Bookish.Models;

namespace Bookish.DatabaseInterfaces
{
    public interface IBooksRepo
    {
        IEnumerable<BookModel> GetBooks();
        IEnumerable<BookModel> GetBooks(int user_id);
        IEnumerable<BookCountModel> GetBooksAndStockCount(string sortBy, bool ascending);
        bool Insert(BookModel book);
        bool Update(BookModel book);
        bool Delete(int id);
    }
}