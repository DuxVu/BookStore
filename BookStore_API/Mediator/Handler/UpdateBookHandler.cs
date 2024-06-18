using BookStore_API.Mediator.Command;
using BookStore_API.Models.DTO;
using BookStore_API.Models;
using MediatR;
using AutoMapper;
using BookStore_API.Repository.IRepository;
using BookStore_API.Exceptions;
using System.Net;

namespace BookStore_API.Mediator.Handler
{
    public class UpdateBookHandler : IRequestHandler<UpdateBookCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _dbBook;

        public UpdateBookHandler(IMapper mapper, IBookRepository dbBook)
        {
            _mapper = mapper;
            _dbBook = dbBook;
        }

        public async Task<bool> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            if (request.BookUpdateDTO == null)
            {
                throw new CustomValidationException(HttpStatusCode.BadRequest, "Bad request.", DateTime.Now);
            }

            Book book = _mapper.Map<Book>(request.BookUpdateDTO);

            await _dbBook.UpdateAsync(book);
            return true;
        }
    }
}
