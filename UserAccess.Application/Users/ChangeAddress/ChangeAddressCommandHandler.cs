using ErrorOr;
using MediatR;
using UserAccess.Application.Common;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;

namespace UserAccess.Application.Users.ChangeAddress;

internal sealed class ChangeAddressCommandHandler : ICommandRequestHandler<ChangeEmailCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly AuthorizationService _authorizationService;

    public ChangeAddressCommandHandler(IUserRepository userRepository, AuthorizationService authorizationService)
    {
        _userRepository = userRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(UserId.Create(request.Id));

        if (user is null)
        {
            return UserErrorsCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(user.Id.Value);

        if (authorizationService.IsError)
        {
            return UserErrorsCodes.CannotChangeAddress;
        }

        var update = User.Update(
            user.Id,
            user.Login,
            user.Password.Value,
            user.Email,
            user.FirstName,
            user.LastName,
            request.Address,
            user.Roles,
            user.CreatedDateTime,
            DateTime.UtcNow);

        await _userRepository.UpdateAsync(update);

        return Unit.Value;
    }
}