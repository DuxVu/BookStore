using BookStore_API.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Mediator.Command
{
    public class DeleteAuthorCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }
}
