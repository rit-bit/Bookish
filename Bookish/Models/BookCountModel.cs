using System.Collections;
using System.Collections.Generic;

namespace Bookish.Models
{
    public class BookCountModel
    {
        public int id { get; set; }
        public string isbn { get; set; }
        public string title { get; set; }
        public string primary_author { get; set; }
        public string additional_authors { get; set; }
        public int count { get; set; }
    }

    public class AllBooksCountModel
    {
        public IEnumerable<BookCountModel> books;
    }
}