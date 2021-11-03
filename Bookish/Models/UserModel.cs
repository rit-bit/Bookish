namespace Bookish.Models
{
    public class UserModel
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email_address { get; set; }
        public decimal balance { get; set; } = 0;
    }
}