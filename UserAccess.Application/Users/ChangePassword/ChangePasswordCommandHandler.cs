using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;
using UserAccess.Domain.Common;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.ChangePassword;

internal sealed class ChangePasswordCommandHandler : ICommandRequestHandler<ChangePasswordCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;

    public ChangePasswordCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(UserId.Create(request.Id));

        if (user is null)
        {
            return Error.NotFound("User.NotFound", "User was not found");
        }

        if (user.Password != Password.CreateUnique(request.OldPassword))
        {
            return Error.Validation("Password.NotCorrect", "The actual password you introduced is not your actual password");
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