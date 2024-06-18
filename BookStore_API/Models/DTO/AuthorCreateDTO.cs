using System.ComponentModel.DataAnnotations;

namespace BookStore_API.Models.DTO
{
    public class AuthorCreateDTO
    {
        [Required]
        public string Name { get; set; }
    }
}
