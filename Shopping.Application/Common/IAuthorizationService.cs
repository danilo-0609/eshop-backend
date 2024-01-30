using ErrorOr;
using MediatR;

namespace Shopping.Application.Common;

internal interface IAuthorizationService
{
    ErrorOr<Unit> IsUserAuthorized(Guid userId);

    bool IsAdmin();
}