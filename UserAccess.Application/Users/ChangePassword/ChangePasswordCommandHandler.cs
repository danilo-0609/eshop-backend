using UserAccess.Application.Common;
using ErrorOr;
using MediatR;
using UserAccess.Domain.Common;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;
using UserAccess.Application.Abstractions;

namespace UserAccess.Application.Users.ChangePassword;

internal sealed class ChangePasswordCommandHandler : ICommandRequestHandler<ChangePasswordCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthorizationService _authorizationService;

    public ChangePasswordCommandHandler(IUserRepository userRepository, IAuthorizationService authorizationService)
    {
        _userRepository = userRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(UserId.Create(request.Id));

        if (user is null)
        {
            return UserErrorsCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(user.Id.Value);

        if (authorizationService.IsError)
        {
            return UserErrorsCodes.CannotChangePassword;
        }

        if (user.Password != Password.CreateUnique(request.OldPassword))
        {
            return UserErrorsCodes.IncorrectOldPassword;
        }

        var newPassword = Password.CreateUnique(request.NewPassword);

        var update = User.Update(
            user.Id,
            user.Login,
            newPassword.Value,
            user.Email,
            user.FirstName,
            user.LastName,
            user.Address,
            user.Roles,
            user.CreatedDateTime,
            DateTime.UtcNow);

        await _userRepository.UpdateAsync(update);

        return Unit.Value;
    }
}