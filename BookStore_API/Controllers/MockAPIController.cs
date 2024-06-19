using BookStore_API.Mediator.Command;
using BookStore_API.Mediator.Querry;
using BookStore_API.Models.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BookStore_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MockAPIController : ControllerBase
    {
        private readonly ILogger<MockAPIController> _logger;
        private readonly IMediator _mediator;

        public MockAPIController(ILogger<MockAPIController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("CreateMock")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CreateMock([FromHeader] CreateMockCommand createMockCommand)
        {
            await _mediator.Send(createMockCommand);
            return Ok();
        }
    }
}
