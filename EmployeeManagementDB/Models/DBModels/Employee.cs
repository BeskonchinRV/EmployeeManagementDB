﻿using System;
using System.Collections.Generic;

namespace EmployeeManagementDB.Models.DBModels;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? FullName { get; set; }

    public string? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Identifier { get; set; }

    public string? ContactInformation { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<AccessRestriction> AccessRestrictions { get; set; } = new List<AccessRestriction>();

    public virtual ICollection<ContactInformation> ContactInformations { get; set; } = new List<ContactInformation>();

    public virtual ICollection<EmployeeRole> EmployeeRoles { get; set; } = new List<EmployeeRole>();

    public virtual ICollection<EmploymentHistory> EmploymentHistories { get; set; } = new List<EmploymentHistory>();

    public virtual ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();

    public virtual ICollection<WorkSchedule> WorkSchedules { get; set; } = new List<WorkSchedule>();
}
