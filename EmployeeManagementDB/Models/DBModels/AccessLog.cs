using System;
using System.Collections.Generic;

namespace EmployeeManagementDB.Models.DBModels;

public partial class AccessLog
{
    public int LogId { get; set; }

    public int? AccountId { get; set; }

    public DateOnly? AccessTime { get; set; }

    public string? ActionPerformed { get; set; }

    public virtual UserAccount? Account { get; set; }
}
