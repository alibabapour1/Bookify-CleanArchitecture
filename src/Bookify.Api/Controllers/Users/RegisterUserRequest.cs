namespace Bookify.Api.Controllers.Users;

public record RegisterUserRequest(string FirstName,string LastName,string EmailAddress,string Password);