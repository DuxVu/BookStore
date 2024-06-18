using BookStore_API.Models.DTO;
using MediatR;

namespace BookStore_API.Mediator.Command
{
    public class UpdateAuthorCommand : IRequest<bool>
    {
        public AuthorUpdateDTO AuthorUpdateDTO { get; set; }
    }
}
