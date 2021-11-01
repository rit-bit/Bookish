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
}