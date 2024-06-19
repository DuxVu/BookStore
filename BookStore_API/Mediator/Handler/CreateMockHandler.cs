using BookStore_API.Mediator.Command;
using BookStore_API.Models;
using BookStore_API.Repository.IRepository;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace BookStore_API.Mediator.Handler
{
    public class CreateMockHandler : IRequestHandler<CreateMockCommand, Unit>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IAuthorRepository _authorRepository;

        public CreateMockHandler(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            _bookRepository = bookRepository;
            _authorRepository = authorRepository;
        }

        public async Task<Unit> Handle(CreateMockCommand request, CancellationToken cancellationToken)
        {
            var authors = new List<Author>
            {
            new Author { Name = "J.R.R. Tolkien", IsActive = true, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now },
            new Author { Name = "J.K. Rowling", IsActive = true, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now },
            new Author { Name = "George R.R. Martin", IsActive = true, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now },
            new Author { Name = "C.S. Lewis", IsActive = true, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now },
            new Author { Name = "J.D. Salinger", IsActive = true, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now }
            };
            foreach (Author author in authors)
            {
                await _authorRepository.CreateAsync(author);
            }
            var books = new List<Book>
            {
                new Book { Title = "Lord of the rings", Subtitle = "The Fellowship of the Ring", AuthorId = authors[0].AuthorId, IsActive = true, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now },
                new Book { Title = "Lord of the rings", Subtitle = "The Two Towers", AuthorId = authors[0].AuthorId, IsActive = true, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now },
                new Book { Title = "Lord of the rings", Subtitle = "The Return of the King", AuthorId = authors[0].AuthorId, IsActive = true, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now },
                new Book { Title = "Harry Potter", Subtitle = "Harry Potter and the Philosopher's Stone", AuthorId = authors[1].AuthorId, IsActive = true, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now },
                new Book { Title = "Harry Potter", Subtitle = "Harry Potter and the Chamber of Secrets", AuthorId = authors[1].AuthorId, IsActive = true, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now },
                new Book { Title = "Harry Potter", Subtitle = "Harry Potter and the Prisoner of Azkaban", AuthorId = authors[1].AuthorId, IsActive = true, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now },
                new Book { Title = "A Game of Thrones", AuthorId = authors[2].AuthorId, IsActive = true, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now },
                new Book { Title = "A Clash of Kings", AuthorId = authors[2].AuthorId, IsActive = true, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now },
                new Book { Title = "The Lion, the Witch and the Wardrobe", AuthorId = authors[3].AuthorId, IsActive = true, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now },
                new Book { Title = "The Catcher in the Rye", AuthorId = authors[4].AuthorId, IsActive = true, CreatedDate = DateTime.Now, ModifyDate = DateTime.Now }
            };
            foreach (var book in books)
            {
                await _bookRepository.CreateAsync(book);
            }
            return Unit.Value;
        }        
    }
}
