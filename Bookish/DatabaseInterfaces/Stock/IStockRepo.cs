using System.Collections;
using System.Collections.Generic;
using Bookish.Models;

namespace Bookish.DatabaseInterfaces
{
    public interface IStockRepo
    {
        IEnumerable<StockModel> GetStock();
        IEnumerable<CopyCountModel> GetAllActiveCopies();
        IEnumerable<StockModel> GetActiveCopies(int id);
        bool Insert(StockModel stockModel);
        bool Update(StockModel stockModel);
        bool Decommission(StockModel stockModel);
        int DecommissionBookStock(int bookId);
    }
}