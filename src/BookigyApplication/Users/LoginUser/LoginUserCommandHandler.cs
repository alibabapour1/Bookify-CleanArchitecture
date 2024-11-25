using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Authentication;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users;

namespace Bookify.Application.Users.LoginUser;

public class LoginUserCommandHandler : ICommandHandler<LoginUserCommand,AccessToken>
{
    private readonly IJwtService _jwtService;

    public LoginUserCommandHandler(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    public async Task<Result<AccessToken>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var result = await _jwtService.GetAccessTokenAsync(request.UserName,
            request.Password,
            cancellationToken);
        return result.IsFailure 
            ? Result.Failure<AccessToken>(UserErrors.InvalidCredentials)
            : new AccessToken(result.Value);
    }
}