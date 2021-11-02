namespace Bookish.Models
{
    public class BookModel
    {
        public int id { get; set; }
        public string isbn { get; set; }
        public string title { get; set; }
        public string primary_author { get; set; }
        public string additional_authors { get; set; }
    }
}