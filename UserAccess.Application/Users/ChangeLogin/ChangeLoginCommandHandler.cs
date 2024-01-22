using BuildingBlocks.Application;
using BuildingBlocks.Application.Commands;
using ErrorOr;
using MediatR;
using UserAccess.Domain.Users;
using UserAccess.Domain.Users.Errors;

namespace UserAccess.Application.Users.ChangeLogin;

internal sealed class ChangeLoginCommandHandler : ICommandRequestHandler<ChangeLoginCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository _userRepository;
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public ChangeLoginCommandHandler(IUserRepository userRepository, IExecutionContextAccessor executionContextAccessor)
    {
        _userRepository = userRepository;
        _executionContextAccessor = executionContextAccessor;
    }

    public async Task<ErrorOr<Unit>> Handle(ChangeLoginCommand request, CancellationToken cancellationToken)
    {
        User? user = await _userRepository.GetByIdAsync(UserId.Create(request.Id));

        if (user is null)
        {
            return UserErrorsCodes.NotFound;
        }

        if (_executionContextAccessor.UserId != user.Id.Value)
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