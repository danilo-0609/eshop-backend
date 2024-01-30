using BuildingBlocks.Application;
using ErrorOr;
using MediatR;

namespace UserAccess.Application;

internal sealed class AuthorizationService
{
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public AuthorizationService(IExecutionContextAccessor executionContextAccessor)
    {
        _executionContextAccessor = executionContextAccessor;
    }

    internal ErrorOr<Unit> IsUserAuthorized(Guid userId)
    {
        if (_executionContextAccessor.UserId == userId)
        {
            return Unit.Value;
        }

        return Error.Unauthorized();
    }

    internal bool IsAdmin()
    {
        return _executionContextAccessor.IsAdmin;
    }
}
