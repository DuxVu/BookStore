using AutoMapper;
using BookStore_API.Exceptions;
using BookStore_API.Mediator.Command;
using BookStore_API.Models;
using BookStore_API.Models.DTO;
using BookStore_API.Repository.IRepository;
using MediatR;
using System.Net;

namespace BookStore_API.Mediator.Handler
{
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, BookDTO>
    {
        private readonly IMapper _mapper;
        private readonly IBookRepository _dbBook;
        public CreateBookHandler(IBookRepository dbBook, IMapper mapper)
        {
            _dbBook = dbBook;
            _mapper = mapper;
        }

        public async Task<BookDTO> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            if (await _dbBook.GetAsync(x => x.Ttitle.ToLower() == request.BookCreateDTO.Ttitle.ToLower()) != null)
            {
                throw new CustomValidationException(HttpStatusCode.BadRequest, "The book with that name already exists!", DateTime.Now);
            }
            if (request.BookCreateDTO == null)
            {
                throw new CustomValidationException(HttpStatusCode.BadRequest, "Bad request!", DateTime.Now);
            }

            Book book = _mapper.Map<Book>(request.BookCreateDTO);

            await _dbBook.CreateAsync(book);
            return _mapper.Map<BookDTO>(book);
        }
    }
}
