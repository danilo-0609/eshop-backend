using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.ChangeAddress;
internal sealed class ChangeAddressCommandHandler : ICommandRequestHandler<ChangeEmailCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;

    public ChangeAddressCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(UserId.Create(request.Id));

        if (user is null)
        {
            return Error.NotFound("User.NotFound", "User was not found");
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