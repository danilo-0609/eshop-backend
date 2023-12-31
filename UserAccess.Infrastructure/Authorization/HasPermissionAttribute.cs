using Microsoft.AspNetCore.Authorization;
using UserAccess.Domain.Enums;

namespace UserAccess.Infrastructure.Authorization;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permissions permission)
        : base(policy: permission.ToString())
    {
    }
}
