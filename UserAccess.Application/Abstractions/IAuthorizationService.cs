using ErrorOr;
using MediatR;

namespace UserAccess.Application.Abstractions;

internal interface IAuthorizationService
{
    ErrorOr<Unit> IsUserAuthorized(Guid userId);

    bool IsAdmin();
}
