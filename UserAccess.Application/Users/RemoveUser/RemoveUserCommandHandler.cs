using UserAccess.Application.Common;
using ErrorOr;
using MediatR;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;

namespace UserAccess.Application.Users.RemoveUser;

internal sealed class RemoveUserCommandHandler : ICommandRequestHandler<RemoveUserCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly AuthorizationService _authorizationService;

    public RemoveUserCommandHandler(IUserRepository userRepository, AuthorizationService authorizationService)
    {
        _userRepository = userRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(UserId.Create(request.UserId));

        if (user is null)
        {
            return UserErrorsCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(user.Id.Value);

        if (authorizationService.IsError && _authorizationService.IsAdmin() is false)
        {
            return UserErrorsCodes.CannotRemove;
        }

        await _userRepository.RemoveAsync(user);

        return Unit.Value;
    }
}
