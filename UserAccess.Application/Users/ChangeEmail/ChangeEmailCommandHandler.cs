using BuildingBlocks.Application;
using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;
using UserAccess.Domain.Users;

namespace UserAccess.Application.Users.ChangeEmail;
internal sealed class ChangeEmailCommandHandler : ICommandRequestHandler<ChangeEmailCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public ChangeEmailCommandHandler(IUserRepository userRepository, IExecutionContextAccessor executionContextAccessor)
    {
        _userRepository = userRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<ErrorOr<Unit>> Handle(ChangeEmailCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(UserId.Create(request.Id));

        if (user is null)
        {
            return Error.NotFound("User.NotFound", "User was not found");
        }
        
        if (user.Id.Value != _executionContextAccessor.UserId)
        {
            return Error.Unauthorized("User.ChangeEmail", "Cannot change if you are not the same user");
        }

        var update = User.Update(
            user.Id,
            user.Login,
            user.Password.Value,
            request.Email,
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
