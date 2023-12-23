using System;
using System.Collections.Generic;

namespace UserAccess.Domain;

public partial class Permission
{
    public int PermissionId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Role> Roles { get; } = new List<Role>();
}
