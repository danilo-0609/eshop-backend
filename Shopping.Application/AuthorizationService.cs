using BuildingBlocks.Application;
using ErrorOr;
using MediatR;

namespace Shopping.Application;

internal sealed class AuthorizationService
{
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public AuthorizationService(IExecutionContextAccessor executionContextAccessor)
    {
        _executionContextAccessor = executionContextAccessor;
    }

    internal ErrorOr<Unit> IsUserAuthorized(Guid customerId)
    {
        if (_executionContextAccessor.UserId == customerId)
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
