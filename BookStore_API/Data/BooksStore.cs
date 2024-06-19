using BookStore_API.Models.DTO;

namespace BookStore_API.Data
{
    public class BooksStore
    {
        public static List<BookDTO> booksList = new List<BookDTO>()
        {
            new BookDTO{BookId = 1, Title = "Lord of the rings"},
            new BookDTO{BookId = 2, Title = "Harry Potter"}
        };
    }
}
