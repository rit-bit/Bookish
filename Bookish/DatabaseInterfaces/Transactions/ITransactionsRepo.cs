using System;

namespace Bookish.DatabaseInterfaces.Transactions
{
    public interface ITransactionsRepo
    {
        bool CheckIn(int stock_id);
        bool CheckOut(int stock_id, int user_id, DateTime? due_back);
    }
}