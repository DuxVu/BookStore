using BookStore_API.Controllers;
using BookStore_API.Mediator.Command;
using BookStore_API.Mediator.Querry;
using BookStore_API.Models.DTO;
using FakeItEasy;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore_Test
{
    public class AuthorAPIControllerTest
    {
        private readonly ILogger<AuthorAPIController> _logger;
        private readonly IMediator _mediator;
        private readonly AuthorAPIController _controller;

        public AuthorAPIControllerTest()
        {
            _logger = A.Fake<ILogger<AuthorAPIController>>();
            _mediator = A.Fake<IMediator>();
            _controller = new AuthorAPIController(_logger, _mediator);
        }

        [Fact]
        public async Task GetAuthorsTest()
        {
            // Arrange
            var fakeAuthors = new List<AuthorDTO>
        {
            new AuthorDTO { AuthorId = 1, Name = "Author 1" },
            new AuthorDTO { AuthorId = 2, Name = "Author 2" }
        };
            var fakeQuery = new GetAllAuthorsQuerry();
            A.CallTo(() => _mediator.Send(fakeQuery, A<CancellationToken>.Ignored)).Returns(Task.FromResult((IEnumerable<AuthorDTO>)fakeAuthors));

            // Act
            var result = await _controller.GetAuthors(fakeQuery);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var authors = Assert.IsAssignableFrom<IEnumerable<AuthorDTO>>(okResult.Value);
            Assert.Equal(fakeAuthors.Count, authors.Count());
        }

        [Fact]
        public async Task GetAuthorTest()
        {
            // Arrange
            var fakeAuthor = new AuthorDTO { AuthorId = 1, Name = "Author 1" };
            var fakeQuery = new GetAuthorQuerry { Id = 1 };

            A.CallTo(() => _mediator.Send(A<GetAuthorQuerry>.That.Matches(q => q.Id == fakeQuery.Id), A<CancellationToken>.Ignored))
                .Returns(Task.FromResult(fakeAuthor));

            // Act
            var result = await _controller.GetAuthor(fakeQuery);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var author = Assert.IsAssignableFrom<AuthorDTO>(okResult.Value);
            Assert.Equal(fakeQuery.Id, author.AuthorId);
        }

        [Fact]
        public async Task CreateAuthorTest()
        {
            // Arrange
            var fakeAuthor = new AuthorDTO { AuthorId = 1, Name = "Author 1" };
            var fakeCommand = new CreateAuthorCommand
            {
                AuthorCreateDTO = new AuthorCreateDTO
                {
                    Name = "Author 1"
                }
            };
            A.CallTo(() => _mediator.Send(fakeCommand, A<CancellationToken>.Ignored)).Returns(Task.FromResult(fakeAuthor));

            // Act
            var result = await _controller.CreateAuthor(fakeCommand);

            // Assert
            var createdResult = Assert.IsType<CreatedAtRouteResult>(result.Result);
            var author = Assert.IsAssignableFrom<AuthorDTO>(createdResult.Value);
            Assert.Equal(fakeAuthor.AuthorId, author.AuthorId);
        }

        [Fact]
        public async Task DeleteAuthorTest()
        {
            // Arrange
            var fakeCommand = new DeleteAuthorCommand { Id = 1 };
            A.CallTo(() => _mediator.Send(fakeCommand, A<CancellationToken>.Ignored)).Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await _controller.DeleteAuthor(fakeCommand);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateAuthorTest()
        {
            // Arrange
            var fakeCommand = new UpdateAuthorCommand
            {
                AuthorUpdateDTO = new AuthorUpdateDTO
                {
                    AuthorId = 1,
                    Name = "Updated Author 1"
                }
            };
            A.CallTo(() => _mediator.Send(fakeCommand, A<CancellationToken>.Ignored)).Returns(Task.FromResult(Unit.Value));

            // Act
            var result = await _controller.UpdateAuthor(fakeCommand);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task GetAuthorWithBooksTest()
        {
            // Arrange
            var fakeAuthorWithBooks = new AuthorDTO
            {
            AuthorId = 1,
            Name = "Author 1",
            Books = new List<BookDTO>
                {
                    new BookDTO { BookId = 1, AuthorId = 1, Title = "Book 1" },
                    new BookDTO { BookId = 2, AuthorId = 1, Title = "Book 2" }
                }
                };
            var fakeQuery = new GetAuthorWithBooksQuery { AuthorId = 1 };
            A.CallTo(() => _mediator.Send(fakeQuery, A<CancellationToken>.Ignored)).Returns(Task.FromResult(fakeAuthorWithBooks));

            // Act
            var result = await _controller.GetAuthorWithBooks(fakeQuery);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var authorWithBooks = Assert.IsAssignableFrom<AuthorDTO>(okResult.Value);
            Assert.Equal(fakeAuthorWithBooks.AuthorId, authorWithBooks.AuthorId);
        }
    }

}
