using System;
using System.Collections.Generic;

namespace EmployeeManagementDB.Models.DBModels;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? Name { get; set; }

    public int? OrganizationId { get; set; }

    public virtual ICollection<EmploymentHistory> EmploymentHistories { get; set; } = new List<EmploymentHistory>();

    public virtual Organization? Organization { get; set; }
}
