using System.Collections.Generic;

namespace Bookish.DatabaseInterfaces
{
    public interface IBooksRepo
    {
        IEnumerable<Book> GetBooks();
        bool Insert(Book book);
        bool Update(Book book);
        bool Delete(Book book);
    }

    public class Book
    {
        public string isbn { get; set; }
        public string title { get; set; }
        public string primary_author { get; set; }
        public string additional_authors { get; set; }
    }
}