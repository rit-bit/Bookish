using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Bookish.Models;
using Dapper;
using Npgsql;

namespace Bookish.DatabaseInterfaces
{
    public class StockRepo : IStockRepo
    {
        public IEnumerable<StockModel> GetStock()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<CopyCountModel> GetAllActiveCopies()
        {
            using var db = DatabaseConnection.GetConnection();
            return db.Query<CopyCountModel>($"SELECT books.id, COUNT(*) FROM books " +
                                            $"RIGHT JOIN stock ON books.id = stock.book_id " +
                                            $"WHERE active = true GROUP BY books.id");
        }

        public IEnumerable<StockModel> GetActiveCopies(int id)
        {
            using var db = DatabaseConnection.GetConnection();
            return db.Query<StockModel>($"SELECT * FROM stock WHERE book_id = {id} AND active = true");
        }
        
        public IEnumerable<StockTransactionModel> GetAllCopies(int id)
        {
            using var db = DatabaseConnection.GetConnection();
            return db.Query<StockTransactionModel>(
                $"WITH latest_loans AS ( " +
                    "SELECT t1.*, CASE WHEN t1.checked_in IS NULL THEN true ELSE false END AS is_on_loan " +
                    "FROM transactions t1 " +
                    "JOIN( " +
                    "SELECT stock_id, MAX(checked_out) AS max_c3 " +
                    "FROM transactions " +
                    "GROUP BY stock_id) t2 " +
                    "ON t1.stock_id = t2.stock_id AND t1.checked_out = t2.max_c3 " +
                ") " +
            "SELECT stock.id, stock.description, stock.active, " +
                "(CASE WHEN latest_loans.is_on_loan IS true THEN users.first_name ELSE null END) AS first_name, " +
                "(CASE WHEN latest_loans.is_on_loan IS true THEN latest_loans.due_back ELSE null END) AS due_back " +
            "FROM stock " +
            "LEFT JOIN latest_loans ON stock.id = latest_loans.stock_id " +
            "LEFT JOIN users ON users.id = latest_loans.user_id " +
            $"WHERE stock.book_id = {id}" + 
            $"ORDER BY latest_loans.checked_out DESC");
        }

        public IEnumerable<UserLoanModel> GetLoansForUser(int user_id)
        {
            using var db = DatabaseConnection.GetConnection();
            return db.Query<UserLoanModel>(
                "SELECT books.title, books.primary_author, stock.id, transactions.checked_out, transactions.due_back, transactions.late_fee " +
                "FROM transactions " + 
                "JOIN stock ON transactions.stock_id = stock.id " + 
                "JOIN books ON stock.book_id = books.id " + 
                $"WHERE transactions.user_id = {user_id} " +
                "AND transactions.checked_in IS NULL");
        }

        public bool Insert(StockModel stockModel)
        {
            using var db = DatabaseConnection.GetConnection();
            var transaction = db.BeginTransaction();
            var rowsAffected = 0;
            try
            {
                rowsAffected = new NpgsqlCommand(
                    $"INSERT INTO stock (book_id, description, active) VALUES (" +
                    $"'{stockModel.book_id}', " +
                    $"'{stockModel.description}', " +
                    $"'{stockModel.is_active}')",
                    db, transaction).ExecuteNonQuery();

                transaction.Commit();
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
            }

            return rowsAffected == 1;
        }

        public bool Update(StockModel stockModel)
        {
            throw new System.NotImplementedException();
        }

        public bool SetActive(int stockId, bool active)
        {
            using var db = DatabaseConnection.GetConnection();
            var transaction = db.BeginTransaction();
            var rowsAffected = 0;
            try
            {
                rowsAffected = new NpgsqlCommand(
                    $"UPDATE stock SET active = {active} WHERE id = {stockId}",
                    db, transaction).ExecuteNonQuery();

                transaction.Commit();
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
            }

            return rowsAffected == 1;
        }

        public int DecommissionBookStock(int book_id)
        {
            using var db = DatabaseConnection.GetConnection();
            var transaction = db.BeginTransaction();
            var rowsAffected = 0;
            try
            {
                rowsAffected = new NpgsqlCommand(
                        $"UPDATE stock SET active = false WHERE book_id = {book_id}",
                    db, transaction).ExecuteNonQuery();

                transaction.Commit();
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
            }

            return rowsAffected;
        }
    }
}