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
    public class CreateAuthorHandler : IRequestHandler<CreateAuthorCommand, AuthorDTO>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _dbAuthor;
        public CreateAuthorHandler(IAuthorRepository dbAuthor, IMapper mapper)
        {
            _dbAuthor = dbAuthor;
            _mapper = mapper;
        }

        public async Task<AuthorDTO> Handle(CreateAuthorCommand request, CancellationToken cancellationToken)
        {
            if (await _dbAuthor.GetAsync(x => x.Name.ToLower() == request.AuthorCreateDTO.Name.ToLower()) != null)
            {
                throw new CustomValidationException(HttpStatusCode.BadRequest, "The author with that name already exists!", DateTime.Now);
            }
            if (request.AuthorCreateDTO == null)
            {
                throw new CustomValidationException(HttpStatusCode.BadRequest, "Bad request!", DateTime.Now);
            }

            Author author = _mapper.Map<Author>(request.AuthorCreateDTO);

            await _dbAuthor.CreateAsync(author);
            return _mapper.Map<AuthorDTO>(author);
        }
    }
}
