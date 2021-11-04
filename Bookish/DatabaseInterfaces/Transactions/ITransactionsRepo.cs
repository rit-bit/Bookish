namespace Bookish.DatabaseInterfaces.Transactions
{
    public interface ITransactionsRepo
    {
        void CheckIn(int stock_id);
        void CheckOut(int stock_id, int user_id);
    }
}