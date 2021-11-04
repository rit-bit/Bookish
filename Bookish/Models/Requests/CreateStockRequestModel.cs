namespace Bookish.Models.Requests
{
    public class CreateStockRequestModel
    {
        public int id { get; set; }
        public int book_id { get; set; }
        public string description { get; set; }
        public bool is_active { get; set; } = false;
    }
}