using System;
using System.Collections.Generic;
using System.Data;
using Bookish.Models;
using Dapper;
using Npgsql;

namespace Bookish.DatabaseInterfaces
{
    public class BooksRepo : IBooksRepo
    {
        public IEnumerable<BookModel> GetBooks()
        {
            using var db = DatabaseConnection.GetConnection();
                return db.Query<BookModel>("SELECT * FROM books");
        }

        public bool Insert(BookModel book)
        {
            using var db = DatabaseConnection.GetConnection();
            var transaction = db.BeginTransaction();
            var rowsAffected = 0;
            try
            {
                rowsAffected = new NpgsqlCommand(
                    $"INSERT INTO books (isbn, title, primary_author, additional_authors) VALUES (" +
                    $"'{book.isbn}', " +
                    $"'{book.title}', " +
                    $"'{book.primary_author}', " +
                    $"'{book.additional_authors}')",
                    db, transaction).ExecuteNonQuery();

                transaction.Commit();
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
            }

            return rowsAffected == 1;
        }

        public bool Update(BookModel book)
        {
            using var db = DatabaseConnection.GetConnection();
            var transaction = db.BeginTransaction();
            var rowsAffected = 0;
            try
            {
                rowsAffected = new NpgsqlCommand(
                    $"UPDATE books SET " +
                    $"isbn = '{book.isbn}', " +
                    $"title = '{book.title}', " +
                    $"primary_author = '{book.primary_author}', " +
                    $"additional_authors = '{book.additional_authors}' " +
                    $"WHERE id = {book.id}",
                    db, transaction).ExecuteNonQuery();

                transaction.Commit();
            }
            catch (NpgsqlException e)
            {
                Console.WriteLine(e);
            }

            return rowsAffected == 1;
        }

        public bool Delete(int id)
        {
            using var db = DatabaseConnection.GetConnection();
            var transaction = db.BeginTransaction();
            var rowsAffected = 0;
            try
            {
                rowsAffected = new NpgsqlCommand(
                    $"DELETE FROM books WHERE id = {id}",
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