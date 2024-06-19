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
using Microsoft.AspNetCore.Http.HttpResults;
using MediatR;
using BookStore_API.Mediator.Querry;
using BookStore_API.Mediator.Command;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorAPIController : ControllerBase
    {

        private readonly ILogger<BookStoreAPIController> _logger;
        private readonly IMediator _mediator;

        public AuthorAPIController(ILogger<BookStoreAPIController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("GetAuthors")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<AuthorDTO>>> GetAuthors([FromHeader]GetAllAuthorsQuerry getAllAuthorsQuerry)
        {
            return Ok(await _mediator.Send(getAllAuthorsQuerry));
        }

        [HttpGet("GetAuthor/{Id:int}", Name = "GetAuthor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthorDTO>> GetAuthor([FromRoute] GetAuthorQuerry getAuthorQuerry)
        {
            return Ok(await _mediator.Send(getAuthorQuerry));
        }

        [HttpPost("CreateAuthor")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AuthorDTO>> CreateAuthor([FromBody] CreateAuthorCommand createAuthorCommand)
        {
            var result = await _mediator.Send(createAuthorCommand);
            return CreatedAtRoute("GetAuthor", new { Id = result.AuthorId }, result);
        }


        [HttpDelete("DeleteAuthor/{Id:int}", Name = "DeleteAuthor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteAuthor([FromRoute] DeleteAuthorCommand deleteAuthorCommand)
        {
            await _mediator.Send(deleteAuthorCommand);
            return NoContent();
        }

        [HttpPut("UpdateAuthor/{Id:int}", Name = "UpdateAuthor")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAuthor(int authorId, [FromBody] UpdateAuthorCommand updateAuthorCommand)
        {
            await _mediator.Send(updateAuthorCommand);
            return NoContent();
        }

        [HttpGet("GetAuthorWithBooks/{AuthorId:int}", Name = "GetAuthorWithBooks")]
        public async Task<IActionResult> GetAuthorWithBooks([FromRoute] GetAuthorWithBooksQuery getAuthorWithBooksQuery)
        {
            var author = await _mediator.Send(getAuthorWithBooksQuery);
            return Ok(author);
        }
    }
}
