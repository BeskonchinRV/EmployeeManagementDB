using System;
using System.Collections.Generic;

namespace EmployeeManagementDB.Models.DBModels;

public partial class Organization
{
    public int OrganizationId { get; set; }

    public string? Name { get; set; }

    public string? Inn { get; set; }

    public string? Ogrn { get; set; }

    public int? ParentId { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<EmploymentHistory> EmploymentHistories { get; set; } = new List<EmploymentHistory>();

    public virtual ICollection<Organization> InverseParent { get; set; } = new List<Organization>();

    public virtual Organization? Parent { get; set; }
}
