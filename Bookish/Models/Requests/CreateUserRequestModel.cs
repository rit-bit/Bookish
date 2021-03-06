using System.ComponentModel.DataAnnotations;

namespace Bookish.Models.Requests
{
    public class CreateUserRequestModel
    {
        [StringLength(255)]
        public string first_name { get; set; }
        [StringLength(255)]
        public string last_name { get; set; }
        [StringLength(255)]
        public string email_address { get; set; }
    }
}