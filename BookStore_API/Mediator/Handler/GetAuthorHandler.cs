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
    public class GetAuthorHandler : IRequestHandler<GetAuthorQuerry, AuthorDTO>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _dbAuthor;
        public GetAuthorHandler(IAuthorRepository dbAuthor, IMapper mapper)
        {
            _dbAuthor = dbAuthor;
            _mapper = mapper;
        }
        public async Task<AuthorDTO> Handle(GetAuthorQuerry request, CancellationToken cancellationToken)
        {
            if (request.Id == 0)
            {
                throw new CustomValidationException(HttpStatusCode.BadRequest, "Id cannot be 0", DateTime.Now);
            }

            var author = await _dbAuthor.GetAsync(x => x.AuthorId == request.Id);
            if (author == null)
            {
                throw new CustomValidationException(HttpStatusCode.NotFound, "The book with the given ID does not exist.", DateTime.Now);
            }
            return _mapper.Map<AuthorDTO>(author);
        }
    }
}
