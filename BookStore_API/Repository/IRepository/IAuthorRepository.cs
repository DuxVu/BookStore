using BookStore_API.Models;

namespace BookStore_API.Repository.IRepository
{
    public interface IAuthorRepository : IRepository<Author>
    {
        Task<Author> UpdateAsync(Author entity);
    }
}
