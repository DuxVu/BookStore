﻿using BookStore_API.Models.DTO;
using MediatR;

namespace BookStore_API.Mediator.Querry
{
    public class GetBookByAuthorQuerry : IRequest<List<BookDTO>>
    {
        public int AuthorId { get; set; }
    }
}
