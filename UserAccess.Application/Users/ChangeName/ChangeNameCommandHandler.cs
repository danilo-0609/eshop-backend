using BuildingBlocks.Application;
using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.ChangeName;

internal sealed class ChangeNameCommandHandler : ICommandRequestHandler<ChangeNameCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public ChangeNameCommandHandler(IUserRepository userRepository, IExecutionContextAccessor executionContextAccessor)
    {
        _userRepository = userRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<ErrorOr<Unit>> Handle(ChangeNameCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(UserId.Create(request.Id));

        if (user is null)
        {
            return Error.NotFound("User.NotFound", "User was not found");
        }

        
        if (_executionContextAccessor.UserId != user.Id.Value)
        {
            return Error.Unauthorized("User.CannotChangeName", "Cannot change the other user's name");
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