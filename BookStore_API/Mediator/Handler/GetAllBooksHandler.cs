using AutoMapper;
using BookStore_API.Data;
using BookStore_API.Mediator.Querry;
using BookStore_API.Models;
using BookStore_API.Models.DTO;
using BookStore_API.Repository;
using BookStore_API.Repository.IRepository;
using MediatR;

namespace BookStore_API.Mediator.Handler
{
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuerry, IEnumerable<BookDTO>>
    {
        private readonly IBookRepository _dbBook;
        private readonly IMapper _mapper;
        public GetAllBooksHandler(IBookRepository dbBook, IMapper mapper)
        {
            _dbBook = dbBook;
            _mapper = mapper;
        }
        public async Task<IEnumerable<BookDTO>> Handle(GetAllBooksQuerry request, CancellationToken cancellationToken)
        {
            IEnumerable<Book> booksList = await _dbBook.GetAllAsync();
            return _mapper.Map<List<BookDTO>>(booksList);
        }
    }
}
