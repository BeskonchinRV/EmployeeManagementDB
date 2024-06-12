using EmployeeManagementDB.Models.DBModels;
using System;
using System.Collections.Generic;

namespace EmployeeManagementDB.Models.DBModels;

public partial class UserAccount
{
    public int AccountId { get; set; }

    public int? EmployeeId { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public int? RoleId { get; set; }

    public DateTime? LastLogin { get; set; }

    public DateTime? LastPasswordChange { get; set; }

    public string? Status { get; set; }

    public virtual ICollection<AccessLog> AccessLogs { get; set; } = new List<AccessLog>();

    public virtual Employee? Employee { get; set; }

    public virtual Role? Role { get; set; }
}
