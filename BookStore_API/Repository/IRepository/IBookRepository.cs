using BookStore_API.Models;

namespace BookStore_API.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book> UpdateAsync(Book entity);
    }
}
