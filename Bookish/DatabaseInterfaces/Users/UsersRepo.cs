using System;
using System.Collections.Generic;
using Bookish.Models;
using Dapper;
using Npgsql;

namespace Bookish.DatabaseInterfaces
{
    public class UsersRepo : IUsersRepo
    {
        public IEnumerable<UserModel> GetUsers()
        {
            using var db = DatabaseConnection.GetConnection();
            return db.Query<UserModel>("SELECT * FROM users");
        }

        public bool Insert(UserModel userModel)
        {
            using var db = DatabaseConnection.GetConnection();
            var transaction = db.BeginTransaction();
            var rowsAffected = 0;
            try
            {
                rowsAffected = new NpgsqlCommand(
                    $"INSERT INTO users (first_name, last_name, email_address, balance) VALUES (" +
                    $"'{userModel.first_name}', " +
                    $"'{userModel.last_name}', " +
                    $"'{userModel.email_address}', " +
                    $"'{userModel.balance}')",
                    db, transaction).ExecuteNonQuery();

                transaction.Commit();
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
            }

            return rowsAffected == 1;
        }

        public bool Update(UserModel userModel)
        {
            using var db = DatabaseConnection.GetConnection();
            var transaction = db.BeginTransaction();
            var rowsAffected = 0;
            try
            {
                rowsAffected = new NpgsqlCommand(
                    $"UPDATE users SET " +
                    $"first_name = '{userModel.first_name}', " +
                    $"last_name = '{userModel.last_name}', " +
                    $"email_address = '{userModel.email_address}' " +
                    $"WHERE id = {userModel.id}",
                    db, transaction).ExecuteNonQuery();

                transaction.Commit();
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
            }

            return rowsAffected == 1;
        }

        public bool Delete(UserModel userModel)
        {
            throw new NotImplementedException();
        }
    }
}