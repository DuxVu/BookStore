using BookStore_API.Models.DTO;
using MediatR;

namespace BookStore_API.Mediator.Querry
{
    public class GetBookQuerry : IRequest<BookDTO>
    {
        public int Id { get; set; }
    }
}
