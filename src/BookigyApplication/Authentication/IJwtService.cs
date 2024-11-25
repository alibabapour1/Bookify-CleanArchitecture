using Bookify.Application.Users.LoginUser;
using Bookify.Domain.Abstractions;

namespace Bookify.Application.Authentication;

public interface IJwtService
{
    Task<Result<string>> GetAccessTokenAsync(string userName
        ,string password
        ,CancellationToken cancellationToken);
}