using System.ComponentModel.DataAnnotations;

namespace Bookish.Models.Requests
{
    public class EditUserRequestModel
    {
        [Required]
        public int id { get; set; }
        
        [Required]
        [StringLength(255)]
        public string first_name { get; set; }
        
        [Required]
        [StringLength(255)]
        public string last_name { get; set; }
        
        [Required]
        [StringLength(255)]
        public string email_address { get; set; }
        
        public decimal balance { get; set; }

    }
}