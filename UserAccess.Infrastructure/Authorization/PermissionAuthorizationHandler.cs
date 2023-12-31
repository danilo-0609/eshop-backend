using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.DependencyInjection;
using System.IdentityModel.Tokens.Jwt;

namespace UserAccess.Infrastructure.Authorization;

public sealed class PermissionAuthorizationHandler
    : AuthorizationHandler<PermissionRequirement>
{
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public PermissionAuthorizationHandler(IServiceScopeFactory serviceScopeFactory)
    {
        _serviceScopeFactory = serviceScopeFactory;
    }

    protected async override Task HandleRequirementAsync(
        AuthorizationHandlerContext context, 
        PermissionRequirement requirement)
    {
        string? userId = context.User.Claims.FirstOrDefault(
                x => x.Type == JwtRegisteredClaimNames.Sub)?.Value;
        
        if (!Guid.TryParse(userId, out var id))
        {
            return;
        }

        using IServiceScope scope = _serviceScopeFactory.CreateScope();

        IPermissionService permissionService = scope
            .ServiceProvider
            .GetRequiredService<IPermissionService>();

        var permissions = await permissionService
            .GetPermissionAsync(id);

        if (permissions.Contains(requirement.Permission))
        {
            context.Succeed(requirement);
        }
    }
}
