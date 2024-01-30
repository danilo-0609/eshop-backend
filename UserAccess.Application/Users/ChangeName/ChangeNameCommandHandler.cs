using UserAccess.Application.Common;
using ErrorOr;
using MediatR;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;
using Microsoft.AspNetCore.Authorization;

namespace UserAccess.Application.Users.ChangeName;

internal sealed class ChangeNameCommandHandler : ICommandRequestHandler<ChangeNameCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly AuthorizationService _authorizationService;

    public ChangeNameCommandHandler(IUserRepository userRepository, AuthorizationService authorizationService)
    {
        _userRepository = userRepository;
        _authorizationService = authorizationService;
    }

    public async Task<ErrorOr<Unit>> Handle(ChangeNameCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(UserId.Create(request.Id));

        if (user is null)
        {
            return UserErrorsCodes.NotFound;
        }

        var authorizationService = _authorizationService.IsUserAuthorized(user.Id.Value);

        if (authorizationService.IsError)
        {
            return UserErrorsCodes.CannotChangeName;
        }

        var update = User.Update(
            user.Id,
            user.Login,
            user.Password.Value,
            user.Email,
            request.FirstName,
            request.LastName,
            user.Address,
            user.Roles,
            user.CreatedDateTime,
            DateTime.UtcNow);

        await _userRepository.UpdateAsync(update);

        return Unit.Value;
    }
}