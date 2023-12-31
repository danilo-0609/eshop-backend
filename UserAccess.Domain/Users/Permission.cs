using System;
using System.Collections.Generic;

namespace UserAccess.Domain;

public class Permission
{
    public int PermissionId { get; set; }

    public string Name { get; set; } = null!;
}
