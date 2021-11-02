using System.ComponentModel.DataAnnotations;

namespace Bookish.Models.Requests
{
    public class EditBookRequestModel
    {
        [Required]
        public int id { get; set; }
        
        [StringLength(13)]
        public string isbn { get; set; }

        [Required(ErrorMessage = "Please fill in a title")]
        [StringLength(255)]
        public string title { get; set; }

        [Required(ErrorMessage = "Please fill in an author")]
        [StringLength(255)]
        public string primary_author { get; set; }

        [StringLength(255)]
        public string additional_authors { get; set; }
    }
}