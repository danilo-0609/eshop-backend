using ErrorOr;
using MediatR;

namespace UserAccess.Application.Common;

internal interface IAuthorizationService
{
    ErrorOr<Unit> IsUserAuthorized(Guid userId);

    bool IsAdmin();
}
