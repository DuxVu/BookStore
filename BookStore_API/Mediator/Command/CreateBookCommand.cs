using BookStore_API.Models.DTO;
using MediatR;

namespace BookStore_API.Mediator.Command
{
    public class CreateBookCommand : IRequest<BookDTO>
    {
        public BookCreateDTO BookCreateDTO { get; set; }
    }
}
