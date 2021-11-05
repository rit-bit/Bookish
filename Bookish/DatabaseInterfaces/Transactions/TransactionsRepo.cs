using System;
using Npgsql;

namespace Bookish.DatabaseInterfaces.Transactions
{
    public class TransactionsRepo : ITransactionsRepo
    {
        public bool CheckIn(int stock_id)
        {
            return false;
        }

        public bool CheckOut(int stock_id, int user_id, DateTime? due_back)
        {
            using var db = DatabaseConnection.GetConnection();
            due_back ??= DateTime.Today.AddDays(14);
            var transaction = db.BeginTransaction();
            var rowsAffected = 0;
            try
            {
                rowsAffected = new NpgsqlCommand(
                    $"INSERT INTO transactions (stock_id, user_id, due_back) VALUES (" +
                    $"'{stock_id}', " +
                    $"'{user_id}', " +
                    $"'{due_back}')",
                    db, transaction).ExecuteNonQuery();

                transaction.Commit();
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
            }

            return rowsAffected == 1;
        }
    }
}