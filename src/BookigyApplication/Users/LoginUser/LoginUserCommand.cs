using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Users.LoginUser;

public record LoginUserCommand(string UserName, string Password) : ICommand<AccessToken>
{
    
}