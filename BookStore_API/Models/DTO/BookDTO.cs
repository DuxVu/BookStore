using System.ComponentModel.DataAnnotations;

namespace BookStore_API.Models.DTO
{
    public class BookDTO
    {
        [Required]
        public int BookId { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Ttitle { get; set; }
        public string? Subtitle { get; set; }
    }
}
