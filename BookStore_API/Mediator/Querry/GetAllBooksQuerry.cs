using BookStore_API.Models.DTO;
using MediatR;

namespace BookStore_API.Mediator.Querry
{
    public class GetAllBooksQuerry : IRequest<IEnumerable<BookDTO>>
    {
    }
}
