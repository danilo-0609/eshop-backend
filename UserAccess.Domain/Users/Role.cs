using System;
using System.Collections.Generic;

namespace UserAccess.Domain;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleCode { get; set; } = null!;

    public static Role Administrator = new Role(1, nameof(Administrator));

    public static Role Customer = new Role(1, nameof(Customer));

    public static Role Seller = new Role(1, nameof(Seller));

    public Role(int roleId, string roleCode)
    {
        RoleId = roleId;
        RoleCode = roleCode;
    }

    public Role()
    {
    }

    public virtual ICollection<Permission> Permissions { get; } = new List<Permission>();
}
