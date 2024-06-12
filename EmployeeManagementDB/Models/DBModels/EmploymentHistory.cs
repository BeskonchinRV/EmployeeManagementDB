using System;
using System.Collections.Generic;

namespace EmployeeManagementDB.Models.DBModels;

public partial class EmploymentHistory
{
    public int EmploymentId { get; set; }

    public int? EmployeeId { get; set; }

    public int? OrganizationId { get; set; }

    public int? DepartmentId { get; set; }

    public int? PositionId { get; set; }

    public string? Supervisor { get; set; }

    public DateOnly? StartDate { get; set; }

    public DateOnly? EndDate { get; set; }

    public virtual Department? Department { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Organization? Organization { get; set; }

    public virtual Position? Position { get; set; }
}
