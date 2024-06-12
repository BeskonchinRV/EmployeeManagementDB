using System;
using System.Collections.Generic;

namespace EmployeeManagementDB.Models.DBModels;

public partial class RoleAccessRight
{
    public int RoleAccessRightId { get; set; }

    public int? RoleId { get; set; }

    public int? AccessRightId { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual AccessRight? AccessRight { get; set; }

    public virtual Role? Role { get; set; }
}
