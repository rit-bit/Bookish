using System;
using System.Collections.Generic;
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
            using var db = DatabaseConnection.GetConnection();
            var transaction = db.BeginTransaction();
            var rowsAffected = 0;
            try
            {
                rowsAffected = new NpgsqlCommand(
                    $"INSERT INTO users (first_name, last_name, email_address, balance) VALUES (" +
                    $"'{user.first_name}', " +
                    $"'{user.last_name}', " +
                    $"'{user.email_address}', " +
                    $"'{user.balance}')",
                    db, transaction).ExecuteNonQuery();

                transaction.Commit();
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
            }

            return rowsAffected == 1;
        }

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }

        public bool Delete(User user)
        {
            throw new NotImplementedException();
        }
    }
}