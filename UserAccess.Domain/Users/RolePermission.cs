namespace UserAccess.Domain.Users;

public sealed class RolePermission
{
    public int RoleId {  get; set; }

    public int PermissionId { get; set; }

    public RolePermission(int roleId, int permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public RolePermission()
    {
    }
}
