using Microsoft.EntityFrameworkCore;
using UserAccess.Domain;

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
        ICollection<Role>[] roles = await _context.Users
            .Include(u => u.Roles)
            .ThenInclude(x => x.Permissions)
            .Where(d => d.Id.Value == userId)
            .Select(d => d.Roles)
            .ToArrayAsync();


        return roles
            .SelectMany(r => r)
            .SelectMany(r => r.Permissions)
            .Select(r => r.Name)
            .ToHashSet();
    }
}
