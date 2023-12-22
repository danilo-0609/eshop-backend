namespace UserAccess.Domain.Users;

public sealed class Role 
{
    public int Id { get; set; }

    public string RoleCode { get; private set; }

    public static readonly Role Administrator = new Role(1, nameof(Administrator));

    public static readonly Role Customer = new Role(2, nameof(Customer));

    public static readonly Role Seller = new Role(3, nameof(Seller));

    private Role(int id, string roleCode)
    {
        Id = id;
        RoleCode = roleCode;
    }

    public ICollection<Permission> Permissions { get; set; }

    public Role() { }
}