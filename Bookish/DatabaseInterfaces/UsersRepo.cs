using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Bookish.Models;
using Dapper;

namespace Bookish.DatabaseInterfaces
{
    public class UsersRepo : IUsersRepo
    {
        public IEnumerable<User> GetUsers()
        {
            const string connectionString = "User ID=postgres;Server=localhost,5432;Database=bookish;Pooling=true;Min Pool Size=0;Max Pool Size=100;Connection Lifetime=0;";
            using IDbConnection db = new SqlConnection(connectionString);
            if (db.State == ConnectionState.Closed)
            {
                db.Open();
            }

            return db.Query<User>("select * from users", commandType:CommandType.Text);
        }

        public bool Insert(User user)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(User user)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(User user)
        {
            throw new System.NotImplementedException();
        }
    }
}