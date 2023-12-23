using Microsoft.AspNetCore.Authorization;
using UserAccess.Domain.Enums;

namespace UserAccess.Infrastructure.Authentication;

public sealed class HasPermissionAttribute : AuthorizeAttribute
{
    public HasPermissionAttribute(Permissions permission)
        : base(policy: permission.ToString())
    {
        
    }
}
