using BuildingBlocks.Application;
using Catalog.Application.Common;
using ErrorOr;
using MediatR;

namespace Catalog.Application;

internal sealed class AuthorizationService : IAuthorizationService
{
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public AuthorizationService(IExecutionContextAccessor executionContextAccessor)
    {
        _executionContextAccessor = executionContextAccessor;
    }

    public ErrorOr<Unit> IsUserAuthorized(Guid customerId)
    {
        if (_executionContextAccessor.UserId == customerId)
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
