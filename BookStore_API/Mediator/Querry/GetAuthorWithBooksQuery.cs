using BookStore_API.Models.DTO;
using MediatR;

namespace BookStore_API.Mediator.Querry
{
    public class GetAuthorWithBooksQuery : IRequest<AuthorDTO>
    {
        public int AuthorId { get; set; }
    }
}
