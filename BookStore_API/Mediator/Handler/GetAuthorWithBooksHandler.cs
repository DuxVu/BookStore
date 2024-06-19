using BookStore_API.Exceptions;
using BookStore_API.Mediator.Querry;
using BookStore_API.Models.DTO;
using BookStore_API.Repository.IRepository;
using MediatR;
using System.Net;

namespace BookStore_API.Mediator.Handler
{
    public class GetAuthorWithBooksHandler : IRequestHandler<GetAuthorWithBooksQuery, AuthorDTO>
    {
        private readonly IAuthorRepository _authorRepository;

        public GetAuthorWithBooksHandler(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }
        public async Task<AuthorDTO> Handle(GetAuthorWithBooksQuery request, CancellationToken cancellationToken)
        {
            var author = await _authorRepository.GetAuthorWithBooksAsync(request.AuthorId);

            if (author == null)
            {
                throw new CustomValidationException(HttpStatusCode.BadRequest, "Bad request.", DateTime.Now);
            }
            var authorDTO = new AuthorDTO
            {
                AuthorId = author.AuthorId,
                Name = author.Name,
                Books = author.Books.Select(b => new BookDTO
                {
                    BookId = b.BookId,
                    Title = b.Title,
                    Subtitle = b.Subtitle,
                    AuthorId = b.AuthorId
                }).ToList()
            };
            return authorDTO;
        }
    }
}
