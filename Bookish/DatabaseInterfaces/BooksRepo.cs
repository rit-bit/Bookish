using System.Collections.Generic;
using System.Data;
using Bookish.Models;
using Dapper;
using Npgsql;

namespace Bookish.DatabaseInterfaces
{
    public class BooksRepo : IBooksRepo
    {
        private const string ConnectionString = "Server=akita.zoo.lan;Port=5432;Database=bookish;Username=bookish;Password=pw";
        public IEnumerable<Book> GetBooks()
        {
            using IDbConnection db = new NpgsqlConnection(ConnectionString);
            
            if (db.State == ConnectionState.Closed)
                db.Open();

            return db.Query<Book>("SELECT * FROM books");
        }

        public bool Insert(Book book)
        {
            throw new System.NotImplementedException();
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