using BookStore_API.Models.DTO;
using MediatR;

namespace BookStore_API.Mediator.Command
{
    public class UpdateBookCommand : IRequest<Unit>
    {
        public BookUpdateDTO BookUpdateDTO { get; set; }
    }
}
