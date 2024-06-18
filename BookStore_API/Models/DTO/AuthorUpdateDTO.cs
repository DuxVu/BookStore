using System.ComponentModel.DataAnnotations;

namespace BookStore_API.Models.DTO
{
    public class AuthorUpdateDTO
    {
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
