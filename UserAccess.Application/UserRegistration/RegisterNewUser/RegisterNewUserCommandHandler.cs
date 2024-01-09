using BuildingBlocks.Application.Commands;
using ErrorOr;
using UserAccess.Application.Abstractions;
using UserAccess.Domain.UserRegistrations;

namespace UserAccess.Application.UserRegistration.RegisterNewUser;

internal sealed class RegisterNewUserCommandHandler : ICommandRequestHandler<RegisterNewUserCommand, ErrorOr<Guid>>
{
    private readonly IUserRegistrationRepository _userRegistrationRepository;
    private readonly IUsersCounter _usersCounter;

    public RegisterNewUserCommandHandler(IUserRegistrationRepository userRegistrationRepository, 
        IUsersCounter usersCounter)
    {
        _userRegistrationRepository = userRegistrationRepository;
        _usersCounter = usersCounter;
    }

    public async Task<ErrorOr<Guid>> Handle(RegisterNewUserCommand request, CancellationToken cancellationToken)
     {
        var registerUser = Domain.UserRegistrations
                            .UserRegistration.RegisterNewUser(
                                request.Login,
                                request.Password,
                                request.Email,
                                request.FirstName,
                                request.LastName,
                                request.Address,
                                _usersCounter,
                                DateTime.UtcNow);

        if (registerUser.IsError)
        {
            return registerUser.FirstError;
        }

        await _userRegistrationRepository.AddAsync(registerUser.Value);

        return registerUser.Value.Id.Value;
    }
}
