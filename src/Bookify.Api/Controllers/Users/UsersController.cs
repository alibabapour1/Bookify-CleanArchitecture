using Bookify.Application.Users.LoginUser;
using Bookify.Application.Users.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bookify.Api.Controllers.Users
{
    [Route("api/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ISender _sender;

        public UsersController(ISender sender)
        {
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser(RegisterUserRequest request, CancellationToken cancellationToken)
        {
            var user = new RegisterUserCommand(request.FirstName,
                request.LastName,
                request.EmailAddress,
                request.Password);
            var result = await _sender.Send(user, cancellationToken);
            return result.IsSuccess ? Ok(result.Value) : BadRequest(result.Error);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(LoginUserRequest request
            ,CancellationToken cancellationToken)
        {
            var command = new LoginUserCommand(request.Email, request.Password);
            var result = await _sender.Send(command,cancellationToken);

           return result.IsFailure ? Unauthorized(result.Error) : Ok(result.Value) ;

        }
        

    }
}
