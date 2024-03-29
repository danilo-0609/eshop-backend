﻿using BuildingBlocks.Application;
using ErrorOr;
using MediatR;
using Shopping.Application.Common;

namespace Shopping.Application;

internal sealed class AuthorizationService : IAuthorizationService
{
    private readonly IExecutionContextAccessor _executionContextAccessor;

    public AuthorizationService(IExecutionContextAccessor executionContextAccessor)
    {
        _executionContextAccessor = executionContextAccessor;
    }

    public ErrorOr<Unit> IsUserAuthorized(Guid userAuthorizedId)
    {
        if (_executionContextAccessor.UserId == userAuthorizedId)
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
