using System;
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

    public class StockTransactionModel
    {
        public int id { get; set; }
        public int book_id { get; set; }
        public string description { get; set; }
        public bool active { get; set; }
        public string first_name { get; set; }
        public DateTime due_back { get; set; }
    }

    public class AllBookStock
    {
        public IEnumerable<StockTransactionModel> allBookStock { get; set; }
        
        public BookModel selectedBook { get; set; }
        
        public string sortBy { get; set; }
        public bool ascending { get; set; }
    }
}