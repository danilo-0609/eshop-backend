using BuildingBlocks.Application;
using UserAccess.Application.Common;
using ErrorOr;
using MediatR;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;

namespace UserAccess.Application.Users.ChangeLogin;

internal sealed class ChangeLoginCommandHandler : ICommandRequestHandler<ChangeLoginCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;

    public ChangeLoginCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(ChangeLoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(UserId.Create(request.Id));

        if (user is null)
        {
            return UserErrorsCodes.NotFound;
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