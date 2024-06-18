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
    public class GetAllAuthorHandler : IRequestHandler<GetAllAuthorsQuerry, IEnumerable<AuthorDTO>>
    {
        private readonly IAuthorRepository _dbAuthor;
        private readonly IMapper _mapper;
        public GetAllAuthorHandler(IAuthorRepository dbAuthor, IMapper mapper)
        {
            _dbAuthor = dbAuthor;
            _mapper = mapper;
        }
        public async Task<IEnumerable<AuthorDTO>> Handle(GetAllAuthorsQuerry request, CancellationToken cancellationToken)
        {
            IEnumerable<Author> authorsList = await _dbAuthor.GetAllAsync();
            return _mapper.Map<List<AuthorDTO>>(authorsList);
        }
    }
}
