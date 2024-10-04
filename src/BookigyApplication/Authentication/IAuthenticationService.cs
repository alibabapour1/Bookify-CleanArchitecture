using Bookify.Domain.Users;

namespace Bookify.Application.Authentication;

public interface IAuthenticationService
{
    Task<string> RegisterUserAsync(User user, string password,CancellationToken cancellationToken);
}