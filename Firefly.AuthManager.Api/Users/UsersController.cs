using MediatR;
using Firefly.AuthManager.Users.Find;
using Firefly.AuthManager.Users.Store;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Firefly.AuthManager.Users.Authenticate;

namespace Firefly.AuthManager.Api.Users
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMediator mediator;

        public UsersController(IMediator mediator)
        {
            this.mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpPut]
        public async Task<IActionResult> Store([FromBody] StoreUserRequest request)
        {
            var command = new StoreUserCommand(request);
            var result = await mediator.Send(command);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> Find(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return BadRequest();

            var command = new FindUserQuery(userName);
            var response = await mediator.Send(command);

            if (response.Success)
                return Ok(response);

            return NotFound(response);
        }

        [Route("Auth")]
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserRequest request)
        {
            var command = new AuthenticateUserCommand(request);
            var result = await mediator.Send(command);

            if (result.Success)
                return Ok(result);

            return Unauthorized(result);
        }
    }
}
