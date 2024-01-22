using Microsoft.EntityFrameworkCore;

namespace UserAccess.Infrastructure.Authorization;

internal sealed class PermissionService : IPermissionService
{
    private readonly UserAccessDbContext _context;

    public PermissionService(UserAccessDbContext context)
    {
        _context = context;
    }

    public async Task<HashSet<string>> GetPermissionAsync(Guid userId)
    {
        var permissions = await _context.Permissions
                .FromSqlRaw(
                    "SELECT p.Name " +
                    "FROM dbo.Permissions p " +
                    "INNER JOIN RolePermissions rp ON p.PermissionId = rp.PermissionId " +
                    "INNER JOIN Roles r ON rp.RoleId = r.RoleId " +
                    "INNER JOIN users.UsersRoles ur ON r.RoleId = ur.RoleId " +
                    "INNER JOIN users.Users u ON ur.UserId = u.UserId " +
                    "WHERE u.UserId = {0}",
                    userId.ToString())
                .Select(p => p.Name)
                .ToListAsync();

        return new HashSet<string>(permissions);
    }
}
