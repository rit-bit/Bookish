﻿using System.Collections.Generic;
using Bookish.Models;

namespace Bookish.DatabaseInterfaces
{
    public interface IBooksRepo
    {
        IEnumerable<BookModel> GetBooks();
        IEnumerable<BookCountModel> GetBooksAndStockCount();
        bool Insert(BookModel book);
        bool Update(BookModel book);
        bool Delete(int id);
    }
}