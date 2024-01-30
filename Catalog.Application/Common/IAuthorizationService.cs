using ErrorOr;
using MediatR;

namespace Catalog.Application.Common;

internal interface IAuthorizationService
{
    ErrorOr<Unit> IsUserAuthorized(Guid userId);

    bool IsAdmin();
}