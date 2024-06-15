using BookStore_API.Models;
using BookStore_API.Models.DTO;
using BookStore_API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookStoreAPIController : ControllerBase
    {
        private readonly ILogger<BookStoreAPIController> _logger;

        public BookStoreAPIController(ILogger<BookStoreAPIController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetBooks")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<BookDTO>> GetBooks()
        {
            _logger.LogInformation("Getting All Books");
            return Ok(BooksStore.booksList);
        }

        [HttpGet("GetBook/{Id:int}", Name = "GetBook")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BookDTO> GetBook([FromRoute]int Id)
        {
            if(Id == 0)
            {
                return BadRequest();
            }

            var book = BooksStore.booksList.FirstOrDefault(x => x.BookId == Id);
            if(book == null)
            {
                return NotFound();
            }
            return Ok(book);
        }

        [HttpPost("CreateBook")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BookDTO> CreateBook([FromBody]BookDTO bookDto)
        {
            if (BooksStore.booksList.FirstOrDefault(x => x.Ttitle.ToLower() == bookDto.Ttitle.ToLower()) != null)
            {
                ModelState.AddModelError("CustomError", "Book already exist!");
                return BadRequest(ModelState);
            }
            if (bookDto == null)
            {
                return BadRequest();
            }
            if(bookDto.BookId > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            bookDto.BookId = BooksStore.booksList.OrderByDescending(x => x.BookId).FirstOrDefault().BookId +1;
            BooksStore.booksList.Add(bookDto);

            return CreatedAtRoute("GetBook", new {Id = bookDto.BookId}, bookDto);
        }


        [HttpDelete("DeleteBook/{Id:int}", Name = "DeleteBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteBook(int bookId)
        {
            if(bookId == null)
            {
                return BadRequest();
            }
            var book = BooksStore.booksList.FirstOrDefault(x=>x.BookId == bookId);
            if(book == null)
            {
                return NotFound();
            }
            BooksStore.booksList.Remove(book);
            return NoContent();
        }

        [HttpPut("UpdateBook/{Id:int}", Name = "UpdateBook")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateBook(int bookId, [FromBody] BookDTO bookDto)
        {
            if(bookDto == null || bookId != bookDto.BookId)
            {
                return BadRequest();
            }
            var book = BooksStore.booksList.FirstOrDefault(x=>x.BookId==bookId);
            book.Ttitle = bookDto.Ttitle;
            book.Subtitle = bookDto.Subtitle;
            book.Author = bookDto.Author;

            return NoContent();
        }
    }
}
