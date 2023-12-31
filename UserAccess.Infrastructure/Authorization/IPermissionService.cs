namespace UserAccess.Infrastructure.Authorization;

public interface IPermissionService
{
    Task<HashSet<string>> GetPermissionAsync(Guid userId);
}
