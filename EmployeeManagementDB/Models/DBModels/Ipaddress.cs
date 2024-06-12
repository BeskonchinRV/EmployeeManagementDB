using System;
using System.Collections.Generic;

namespace EmployeeManagementDB.Models.DBModels;

public partial class Ipaddress
{
    public int IpaddressId { get; set; }

    public int? AccessRestrictionId { get; set; }

    public string? Address { get; set; }

    public virtual AccessRestriction? AccessRestriction { get; set; }
}
