using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.ChangeName;
internal sealed class ChangeNameCommandHandler : ICommandRequestHandler<ChangeNameCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;

    public ChangeNameCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(ChangeNameCommand request, CancellationToken cancellationToken)
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