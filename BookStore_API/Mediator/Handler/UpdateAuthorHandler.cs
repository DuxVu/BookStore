using AutoMapper;
using BookStore_API.Exceptions;
using BookStore_API.Mediator.Command;
using BookStore_API.Models;
using BookStore_API.Repository.IRepository;
using MediatR;
using System.Net;

namespace BookStore_API.Mediator.Handler
{
    public class UpdateAuthorHandler : IRequestHandler<UpdateAuthorCommand, bool>
    {
        private readonly IMapper _mapper;
        private readonly IAuthorRepository _dbAuthor;

        public UpdateAuthorHandler(IMapper mapper, IAuthorRepository dbAuthor)
        {
            _mapper = mapper;
            _dbAuthor = dbAuthor;
        }

        public async Task<bool> Handle(UpdateAuthorCommand request, CancellationToken cancellationToken)
        {
            if (request.AuthorUpdateDTO == null)
            {
                throw new CustomValidationException(HttpStatusCode.BadRequest, "Bad request.", DateTime.Now);
            }

            Author author = _mapper.Map<Author>(request.AuthorUpdateDTO);

            await _dbAuthor.UpdateAsync(author);
            return true;
        }
    }
}
