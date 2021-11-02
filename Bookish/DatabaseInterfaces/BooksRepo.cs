using System.Collections.Generic;
using System.Data;
using Bookish.Models;
using Dapper;
using Npgsql;

namespace Bookish.DatabaseInterfaces
{
    public class BooksRepo : IBooksRepo
    {
        private const string ConnectionString = "Server=localhost;Port=5432;Database=bookish;Username=bookish;Password=pw";
        public IEnumerable<Book> GetBooks()
        {
            using IDbConnection db = new NpgsqlConnection(ConnectionString);
            return db.Query<Book>("SELECT * FROM books");
            /*
            const string connectionString = "User ID=postgres;Server=localhost,5432;Database=bookish;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";
            using IDbConnection db = new SqlConnection(connectionString);
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
            }
            
            return db.Query<Book>("select * from books", commandType:CommandType.Text);
            */
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