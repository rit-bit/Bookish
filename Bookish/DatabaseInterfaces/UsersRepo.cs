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
        private const string ConnectionString = "Server=akita.zoo.lan;Port=5432;Database=bookish;Username=bookish;Password=pw";
        public IEnumerable<User> GetUsers()
        {
            using IDbConnection db = new NpgsqlConnection(ConnectionString);
            
            if (db.State == ConnectionState.Closed)
                db.Open();
            
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