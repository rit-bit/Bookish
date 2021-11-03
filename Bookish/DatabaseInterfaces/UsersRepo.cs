using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using Bookish.Models;
using Dapper;
using Npgsql;

namespace Bookish.DatabaseInterfaces
{
    public class UsersRepo : IUsersRepo
    {
        public IEnumerable<User> GetUsers()
        {
            using var db = DatabaseConnection.GetConnection();
            return db.Query<User>("SELECT * FROM users");
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