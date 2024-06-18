using AutoMapper;
using BookStore_API.Controllers;
using BookStore_API.Exceptions;
using BookStore_API.Mediator.Command;
using BookStore_API.Repository.IRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Runtime.CompilerServices;

namespace BookStore_API.Mediator.Handler
{
    public class DeleteAuthorHandler : IRequestHandler<DeleteAuthorCommand, bool>
    {
        private readonly IAuthorRepository _dbAuthor;
        public DeleteAuthorHandler(IAuthorRepository dbAuthor)
        {
            _dbAuthor = dbAuthor;
        }

        public async Task<bool> Handle(DeleteAuthorCommand request, CancellationToken cancellationToken)
        {
            var author = await _dbAuthor.GetAsync(x => x.AuthorId == request.Id);
            if (author == null)
            {
                throw new CustomValidationException(HttpStatusCode.NotFound, "The author with the given ID does not exist.", DateTime.Now);
            }
            await _dbAuthor.DeleteAsync(author);
            return true;
        }
    }
}
