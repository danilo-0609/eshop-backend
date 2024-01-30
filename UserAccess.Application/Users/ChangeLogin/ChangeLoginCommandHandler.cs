using UserAccess.Application.Common;
using ErrorOr;
using MediatR;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;

namespace UserAccess.Application.Users.ChangeLogin;

internal sealed class ChangeLoginCommandHandler : ICommandRequestHandler<ChangeLoginCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IAuthorizationService _authorizationService;

    public ChangeLoginCommandHandler(IUserRepository userRepository, IAuthorizationService authorizationService)
    {
        _userRepository = userRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(ChangeLoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(UserId.Create(request.Id));

        if (user is null)
        {
            return UserErrorsCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(user.Id.Value);

        if (authorizationService.IsError)
        {
            return UserErrorsCodes.CannotChangeLogin;
        }

        var update = User.Update(
            user.Id,
            request.Login,
            user.Password.Value,
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