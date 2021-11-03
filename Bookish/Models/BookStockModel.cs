using System.Collections.Generic;

namespace Bookish.Models
{
    public class StockModel
    {
        public int id { get; set; }
        public int book_id { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
    }

    public class AllBookStock
    {
        public IEnumerable<StockModel> allBookStock { get; set; }
        
        public BookModel selectedBook { get; set; }
        
        public string sortBy { get; set; }
        public bool ascending { get; set; }
    }
}