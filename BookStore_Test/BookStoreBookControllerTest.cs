using AutoMapper;
using BookStore_API.Controllers;
using BookStore_API.Mediator.Command;
using BookStore_API.Mediator.Handler;
using BookStore_API.Mediator.Querry;
using BookStore_API.Models.DTO;
using BookStore_API.Repository.IRepository;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BookStore_Test
{
    public class BookStoreBookControllerTest
    {
        private readonly ILogger<BookStoreAPIController> _logger;
        private readonly IMediator _mediator;
        private readonly BookStoreAPIController _controller;

        public BookStoreBookControllerTest()
        {
            _logger = A.Fake<ILogger<BookStoreAPIController>>();
            _mediator = A.Fake<IMediator>();
            _controller = new BookStoreAPIController(_logger, _mediator);
        }

        [Fact]
        public async Task GetBooksTest()
        {
            // Arrange
            var fakeBooks = new List<BookDTO>
            {
                new BookDTO { BookId = 1, AuthorId = 1, Title = "Test Book 1" },
                new BookDTO { BookId = 2, AuthorId = 2, Title = "Test Book 2" }
            };
            var fakeQuery = new GetAllBooksQuerry();
            A.CallTo(() => _mediator.Send(fakeQuery, A<CancellationToken>.Ignored)).Returns(Task.FromResult((IEnumerable<BookDTO>)fakeBooks));

            // Act
            var result = await _controller.GetBooks(fakeQuery);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var books = Assert.IsAssignableFrom<IEnumerable<BookDTO>>(okResult.Value);
            Assert.Equal(fakeBooks.Count, books.Count());
        }

        [Fact]
        public async Task GetBookTest()
        {
            // Arrange
            var fakeBooks = new List<BookDTO>
            {
                new BookDTO { BookId = 1, AuthorId = 1, Title = "Test Book 1" },
                new BookDTO { BookId = 2, AuthorId = 2, Title = "Test Book 2" }
            };
            var fakeQuery = new GetBookQuerry { Id = 2 };

            A.CallTo(() => _mediator.Send(A<GetBookQuerry>.That.Matches(q => q.Id == fakeQuery.Id), A<CancellationToken>.Ignored))
                        .ReturnsLazily(() => Task.FromResult(fakeBooks.FirstOrDefault(book => book.BookId == fakeQuery.Id)));
            // Act
            var result = await _controller.GetBook(fakeQuery);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var book = Assert.IsAssignableFrom<BookDTO>(okResult.Value);
            Assert.Equal(fakeQuery.Id, book.BookId);
        }

        [Fact]
        public async Task CreateBookTest()
        {
            // Arrange
            var fakeBook = new BookDTO { BookId = 1, AuthorId = 1, Title = "Test Book 1" };
            var fakeCommand = new CreateBookCommand
            {
                BookCreateDTO = new BookCreateDTO
                {
                    Title = "Test Book 1",
                    AuthorId = 1
                }
            };
            A.CallTo(() => _mediator.Send(fakeCommand, A<CancellationToken>.Ignored)).Returns(Task.FromResult(fakeBook));

            // Act
            var result = await _controller.CreateBook(fakeCommand);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(result.Result);
            var book = Assert.IsAssignableFrom<BookDTO>(createdResult.Value);
            Assert.Equal(fakeBook.BookId, book.BookId);
        }


        [Fact]
        public async Task DeleteBookTest()
        {
            // Arrange
            var fakeCommand = new DeleteBookCommand { Id = 1 };
            A.CallTo(() => _mediator.Send(fakeCommand, A<CancellationToken>.Ignored)).Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await _controller.DeleteBook(fakeCommand);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateBookTest()
        {
            // Arrange
            var fakeCommand = new UpdateBookCommand
            {
                BookUpdateDTO = new BookUpdateDTO
                {
                    BookId = 1,
                    AuthorId = 1,
                    Title = "Updated Test Book 1",
                    Subtitle = "Updated Subtitle"
                }
            };
            A.CallTo(() => _mediator.Send(fakeCommand, A<CancellationToken>.Ignored)).Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await _controller.UpdateBook(fakeCommand);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }



        [Fact]
        public async Task GetBookByAuthorTest()
        {
            // Arrange
            var fakeBooks = new List<BookDTO>
            {
                new BookDTO { BookId = 1, AuthorId = 1, Title = "Test Book 1" },
                new BookDTO { BookId = 2, AuthorId = 1, Title = "Test Book 2" }
            };
            var fakeQuery = new GetBookByAuthorQuerry { AuthorId = 1 };
            A.CallTo(() => _mediator.Send(fakeQuery, A<CancellationToken>.Ignored)).Returns(Task.FromResult(fakeBooks));

            // Act
            var result = await _controller.GetBookByAuthor(fakeQuery);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var books = Assert.IsAssignableFrom<IEnumerable<BookDTO>>(okResult.Value);
            Assert.Equal(fakeBooks.Count, books.Count());
        }

    }
}