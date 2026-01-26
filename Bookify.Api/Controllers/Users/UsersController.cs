using Bookify.Application.Users.RegisterUser;
using Bookify.Domain.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Users
{
    [ApiController]
    [Route("api/user")]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register(
            RegisterUserRequest request,
            CancellationToken cancellationToken)
        {
            RegisterUserCommand command = new RegisterUserCommand(
                request.Email,
                request.FirstName,
                request.Surname,
                request.Password);

            Result<Guid> result = await _sender.Send(command, cancellationToken);

            if(result.IsFailure)
                return BadRequest(result.Error);

            return Ok(result.Value);
        }
    }
}
