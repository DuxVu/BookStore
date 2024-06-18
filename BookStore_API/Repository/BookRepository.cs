using BookStore_API.Data;
using BookStore_API.Models;
using BookStore_API.Repository.IRepository;

namespace BookStore_API.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private readonly ApplicationDbContext _db;

        public BookRepository(ApplicationDbContext db)
            : base(db)
        {
            _db = db;
        }

        public async Task<Book> UpdateAsync(Book entity)
        {
            entity.ModifyDate = DateTime.Now;
            _db.Books.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
