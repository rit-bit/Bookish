using System.Collections.Generic;
using Bookish.Models;
using Dapper;

namespace Bookish.DatabaseInterfaces
{
    public class StockRepo : IStockRepo
    {
        public IEnumerable<StockModel> GetStock()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<StockModel> GetCopies(int id)
        {
            using var db = DatabaseConnection.GetConnection();
            return db.Query<StockModel>($"SELECT * FROM stock WHERE book_id = {id}");
        }

        public bool Insert(StockModel stockModel)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(StockModel stockModel)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(StockModel stockModel)
        {
            throw new System.NotImplementedException();
        }
    }
}