using System;
using System.Collections.Generic;

namespace EmployeeManagementDB.Models.DBModels;

public partial class ContactInformation
{
    public int ContactId { get; set; }

    public int? EmployeeId { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public virtual Employee? Employee { get; set; }
}
