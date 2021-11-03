using System.Collections;
using System.Collections.Generic;
using Bookish.Models;

namespace Bookish.DatabaseInterfaces
{
    public interface IStockRepo
    {
        IEnumerable<StockModel> GetStock();
        IEnumerable<StockModel> GetCopies(int id);
        bool Insert(StockModel stockModel);
        bool Update(StockModel stockModel);
        bool Delete(StockModel stockModel);
    }
}