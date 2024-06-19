using System.ComponentModel.DataAnnotations;

namespace BookStore_API.Models.DTO
{
    public class AuthorDTO
    {
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public string Name { get; set; }

        public IEnumerable<BookDTO>? Books { get; set; }
    }
}
