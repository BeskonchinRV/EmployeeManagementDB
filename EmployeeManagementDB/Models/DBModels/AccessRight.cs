using System;
using System.Collections.Generic;

namespace EmployeeManagementDB.Models.DBModels;

public partial class AccessRight
{
    public int AccessRightId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<RoleAccessRight> RoleAccessRights { get; set; } = new List<RoleAccessRight>();
}
