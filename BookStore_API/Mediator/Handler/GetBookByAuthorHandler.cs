using AutoMapper;
using BookStore_API.Exceptions;
using BookStore_API.Mediator.Querry;
using BookStore_API.Models.DTO;
using BookStore_API.Repository.IRepository;
using MediatR;
using System.Net;

namespace BookStore_API.Mediator.Handler
{
    public class GetBookByAuthorHandler : IRequestHandler<GetBookByAuthorQuerry, List<BookDTO>>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _dbBook;

        public GetBookByAuthorHandler(IMapper mapper, IBookRepository dbBook)
        {
            _mapper = mapper;
            _dbBook = dbBook;
        }

        public async Task<List<BookDTO>> Handle(GetBookByAuthorQuerry request, CancellationToken cancellationToken)
        {
            if (request.AuthorId == 0)
            {
                throw new CustomValidationException(HttpStatusCode.BadRequest, "Id cannot be 0", DateTime.Now);
            }

            var book = await _dbBook.GetAllAsync(x => x.AuthorId == request.AuthorId);
            if (book == null)
            {
                throw new CustomValidationException(HttpStatusCode.NotFound, "The book with the given ID does not exist.", DateTime.Now);
            }
            return _mapper.Map<List<BookDTO>>(book);
        }
    }
}
