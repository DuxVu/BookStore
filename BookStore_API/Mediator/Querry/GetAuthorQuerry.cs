using BookStore_API.Models.DTO;
using MediatR;

namespace BookStore_API.Mediator.Querry
{
    public class GetAuthorQuerry : IRequest<AuthorDTO>
    {
        public int Id { get; set; }
    }
}
