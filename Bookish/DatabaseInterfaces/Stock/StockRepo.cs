using System;
using System.Collections;
using System.Collections.Generic;
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

        public IEnumerable<CopyCountModel> GetAllCopies()
        {
            using var db = DatabaseConnection.GetConnection();
            return db.Query<CopyCountModel>($"SELECT books.id, COUNT(*) FROM books " +
                                            $"LEFT JOIN stock ON books.id = stock.book_id " +
                                            $"GROUP BY books.id;");
        }

        public IEnumerable<StockModel> GetCopies(int id)
        {
            using var db = DatabaseConnection.GetConnection();
            return db.Query<StockModel>($"SELECT * FROM stock WHERE book_id = {id}");
        }

        public bool Insert(StockModel stockModel)
        {
            using var db = DatabaseConnection.GetConnection();
            var transaction = db.BeginTransaction();
            var rowsAffected = 0;
            try
            {
                rowsAffected = new NpgsqlCommand(
                    $"INSERT INTO stock (book_id, description) VALUES (" +
                    $"'{stockModel.book_id}', " +
                    $"'{stockModel.description}')",
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

        public bool Delete(StockModel stockModel)
        {
            throw new System.NotImplementedException();
        }

        public int DeleteBookStock(int book_id)
        {
            using var db = DatabaseConnection.GetConnection();
            var transaction = db.BeginTransaction();
            var rowsAffected = 0;
            try
            {
                rowsAffected = new NpgsqlCommand(
                    $"DELETE FROM stock WHERE book_id = {book_id}",
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