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
        
        public IEnumerable<StockModel> GetAllCopies(int id)
        {
            using var db = DatabaseConnection.GetConnection();
            return db.Query<StockModel>($"SELECT stock.id, stock.description, stock.active " +
                                        $"FROM stock WHERE stock.book_id = {id} " +
                                        $"ORDER BY stock.id ASC");
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