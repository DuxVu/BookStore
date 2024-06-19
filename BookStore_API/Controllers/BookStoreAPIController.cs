using BookStore_API.Models;
using BookStore_API.Models.DTO;
using BookStore_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using System.Collections.Generic;
using BookStore_API.Repository.IRepository;
using MediatR;
using BookStore_API.Mediator.Querry;
using BookStore_API.Mediator.Command;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreAPIController : ControllerBase
    {
        private readonly ILogger<BookStoreAPIController> _logger;
        private readonly IMediator _mediator;

        public BookStoreAPIController(ILogger<BookStoreAPIController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("GetBooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetBooks([FromHeader] GetAllBooksQuerry getAllBooksQuerry)
        {
            return Ok(await _mediator.Send(getAllBooksQuerry));
        }

        [HttpGet("GetBook/{Id:int}", Name = "GetBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookDTO>> GetBook([FromRoute] GetBookQuerry getBookQuerry)
        {
            return Ok(await _mediator.Send(getBookQuerry));
        }

        [HttpPost("CreateBook")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BookDTO>> CreateBook([FromBody] CreateBookCommand createBookCommand)
        {
            var result = await _mediator.Send(createBookCommand);
            return CreatedAtRoute("GetBook", new { Id = result.BookId }, result);
        }


        [HttpDelete("DeleteBook/{Id:int}", Name = "DeleteBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBook([FromRoute] DeleteBookCommand createBookCommand)
        {
            await _mediator.Send(createBookCommand);
            return NoContent();
        }

        [HttpPut("UpdateBook/{Id:int}", Name = "UpdateBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBook(int bookId, [FromBody] UpdateBookCommand updateBookCommand)
        {
            await _mediator.Send(updateBookCommand);
            return NoContent();
        }

        [HttpGet("GetBookByAuthorID/{AuthorId:int}", Name = "GetBookByAuthorID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<List<BookDTO>>>GetBookByAuthor([FromRoute] GetBookByAuthorQuerry getBookByAuthorQuerry)
        {
            return Ok(await _mediator.Send(getBookByAuthorQuerry));
        }
    }
}
