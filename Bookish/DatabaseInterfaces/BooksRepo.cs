using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace Bookish.DatabaseInterfaces
{
    public class BooksRepo : IBooksRepo
    {
        public IEnumerable<Book> GetBooks()
        {
            const string connectionString = "User ID=postgres;Server=localhost,5432;Database=bookish;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";
            using IDbConnection db = new SqlConnection(connectionString);
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
            }

            return db.Query<Book>("select * from books", commandType:CommandType.Text);
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