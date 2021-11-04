using System.Collections;
using System.Collections.Generic;
using Bookish.Models;

namespace Bookish.DatabaseInterfaces
{
    public interface IStockRepo
    {
        IEnumerable<StockModel> GetStock();
        IEnumerable<CopyCountModel> GetAllActiveCopies();
        IEnumerable<StockTransactionModel> GetAllCopies(int id);
        IEnumerable<UserLoanModel>  GetLoansForUser(int user_id);
        IEnumerable<StockModel> GetActiveCopies(int id);
        bool Insert(StockModel stockModel);
        bool Update(StockModel stockModel);
        bool SetActive(int stockId, bool active);
        int DecommissionBookStock(int bookId);
    }
}