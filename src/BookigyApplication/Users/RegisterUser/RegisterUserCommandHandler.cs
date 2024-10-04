using Bookify.Application.Abstractions.Messaging;
using Bookify.Application.Authentication;
using Bookify.Domain.Abstractions;
using Bookify.Domain.Users;

namespace Bookify.Application.Users.RegisterUser;

public class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand,Guid>
{
    private readonly IAuthenticationService _authenticationService;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    public RegisterUserCommandHandler(IUnitOfWork unitOfWork, IAuthenticationService authenticationService, IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _authenticationService = authenticationService;
        _userRepository = userRepository;
    }
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        
        var user = User.Create(
            new FirstName(request.FirstName),
            new LastName(request.LastName),
            new Email(request.EmailAddress));

        var userIdentityId = await _authenticationService.RegisterUserAsync(user,
            request.Password,
            cancellationToken);

        user.SetIdentityId(userIdentityId); 

        _userRepository.Add(user);

        await _unitOfWork.SaveChangesAsync(cancellationToken);  

        return user.Id;

    }
}