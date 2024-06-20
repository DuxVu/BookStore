using System.ComponentModel.DataAnnotations;

namespace BookStore_API.Models.DTO
{
    public class BookCreateDTO
    {
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public string Title { get; set; }
        public string? Subtitle { get; set; }
    }
}
