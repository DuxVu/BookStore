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
    public class DeleteBookHandler : IRequestHandler<DeleteBookCommand, bool>
    {
        private readonly IBookRepository _dbBook;
        public DeleteBookHandler(IBookRepository dbBook)
        {
            _dbBook = dbBook;
        }

        public async Task<bool> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _dbBook.GetAsync(x => x.BookId == request.Id);
            if (book == null)
            {
                throw new CustomValidationException(HttpStatusCode.NotFound, "The book with the given ID does not exist.", DateTime.Now);
            }
            await _dbBook.DeleteAsync(book);
            return true;
        }
    }
}
