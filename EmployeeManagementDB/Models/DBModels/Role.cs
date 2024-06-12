using System;
using System.Collections.Generic;

namespace EmployeeManagementDB.Models.DBModels;

public partial class Role
{
    public int RoleId { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();

    public virtual ICollection<RoleAccessRight> RoleAccessRights { get; set; } = new List<RoleAccessRight>();

    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
}
