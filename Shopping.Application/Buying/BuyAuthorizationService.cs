using BuildingBlocks.Application;
using ErrorOr;
using MediatR;
using Shopping.Domain.Buying;

namespace Shopping.Application.Buying;

public sealed class BuyAuthorizationService
{
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public BuyAuthorizationService(IExecutionContextAccessor executionContextAccessor)
    {
        _executionContextAccessor = executionContextAccessor;
    }

    internal ErrorOr<Unit> IsUserAuthorized(Guid customerId)
    {
        if (_executionContextAccessor.UserId == customerId)
        {
            return Unit.Value;
        }

        return BuyErrorCodes.UserNotAuthorizedToAccess;
    }

    internal bool IsAdmin()
    {
        return _executionContextAccessor.IsAdmin;
    }
}
