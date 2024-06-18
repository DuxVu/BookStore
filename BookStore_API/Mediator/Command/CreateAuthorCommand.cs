using BookStore_API.Models.DTO;
using MediatR;

namespace BookStore_API.Mediator.Command
{
    public class CreateAuthorCommand : IRequest<AuthorDTO>
    {
        public AuthorCreateDTO AuthorCreateDTO { get; set; }
    }
}
