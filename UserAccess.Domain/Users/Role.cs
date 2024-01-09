using UserAccess.Domain.Users;

namespace UserAccess.Domain;

public class Role
{
    public int RoleId { get; set; }

    public string RoleCode { get; set; } = null!;

    public static Role Administrator = new Role(1, nameof(Administrator));

    public static Role Customer = new Role(2, nameof(Customer));

    public static Role Seller = new Role(3, nameof(Seller));

    public ICollection<User> Users { get; } = new List<User>();

    public Role(int roleId, string roleCode)
    {
        RoleId = roleId;
        RoleCode = roleCode;
    }

    public Role()
    {
    }

    public ICollection<Permission> Permissions { get; } = new List<Permission>();
}
