using System.Collections;
using System.Collections.Generic;
using Bookish.DatabaseInterfaces;

namespace Bookish.Models
{
    public class BooksModel
    {
        public IEnumerable<BookModel> books { get; set; }
    }
}