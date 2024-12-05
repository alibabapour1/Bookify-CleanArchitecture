using Bookify.Domain.Users;

namespace Bookify.Infrastructure.Authorization;

public class UserRolesResponse
{
    public Guid Id { get; init; }

    public List<Role> Roles { get; init; } = [];
}