using System;
using System.Collections.Generic;

namespace EmployeeManagementDB.Models.DBModels;

public partial class EmployeeRole
{
    public int EmployeeRoleId { get; set; }

    public int? EmployeeId { get; set; }

    public int? RoleId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Role? Role { get; set; }
}
