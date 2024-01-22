using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;

namespace UserAccess.Application.Users.RemoveUser;

internal sealed class RemoveUserCommandHandler : ICommandRequestHandler<RemoveUserCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;

    public RemoveUserCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(RemoveUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(UserId.Create(request.UserId));

        if (user is null)
        {
            return UserErrorsCodes.NotFound;
        }

        await _userRepository.RemoveAsync(user);

        return Unit.Value;
    }
}
