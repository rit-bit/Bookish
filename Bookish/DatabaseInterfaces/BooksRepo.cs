using System.Collections.Generic;
using System.Data;
using Bookish.Models;
using Dapper;
using Npgsql;

namespace Bookish.DatabaseInterfaces
{
    public class BooksRepo : IBooksRepo
    {
        private DatabaseConnection _database;
        
        public IEnumerable<Book> GetBooks()
        {
            return _database.db.Query<Book>("SELECT * FROM books");
        }

        public bool Insert(Book book)
        {
            return true;
        }

        public bool Update(Book book)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(Book book)
        {
            throw new System.NotImplementedException();
        }
    }
}