using Bookify.Application.Abstractions.Messaging;

namespace Bookify.Application.Users.RegisterUser;

public record RegisterUserCommand(string FirstName,
    string LastName,
    string EmailAddress,
    string Password) :ICommand<Guid> ;