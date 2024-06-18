using AutoMapper;
using Azure.Core;
using BookStore_API.Exceptions;
using BookStore_API.Mediator.Querry;
using BookStore_API.Models.DTO;
using BookStore_API.Repository.IRepository;
using MediatR;
using System.Net;

namespace BookStore_API.Mediator.Handler
{
    public class GetBookHandler : IRequestHandler<GetBookQuerry, BookDTO>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _dbBook;
        public GetBookHandler(IBookRepository dbBook, IMapper mapper)
        {
            _dbBook = dbBook;
            _mapper = mapper;
        }
        public async Task<BookDTO> Handle(GetBookQuerry request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                throw new CustomValidationException(HttpStatusCode.BadRequest, "Id cannot be 0", DateTime.Now);
            }

            var book = await _dbBook.GetAsync(x => x.BookId == request.Id);
            if (book == null)
            {
                throw new CustomValidationException(HttpStatusCode.NotFound, "The book with the given ID does not exist.", DateTime.Now);
            }
            return _mapper.Map<BookDTO>(book);
        }
    }
}
