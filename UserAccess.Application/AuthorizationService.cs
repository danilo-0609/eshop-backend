using BuildingBlocks.Application;
using ErrorOr;
using MediatR;
using UserAccess.Application.Common;

namespace UserAccess.Application;

internal sealed class AuthorizationService : IAuthorizationService
{
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public AuthorizationService(IExecutionContextAccessor executionContextAccessor)
    {
        _executionContextAccessor = executionContextAccessor;
    }

    public ErrorOr<Unit> IsUserAuthorized(Guid userId)
    {
        if (_executionContextAccessor.UserId == userId)
        {
            return Unit.Value;
        }

        return Error.Unauthorized();
    }

    public bool IsAdmin()
    {
        return _executionContextAccessor.IsAdmin;
    }
}
