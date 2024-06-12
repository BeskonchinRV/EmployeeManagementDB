using System;
using System.Collections.Generic;

namespace EmployeeManagementDB.Models.DBModels;

public partial class AccessRestriction
{
    public int RestrictionId { get; set; }

    public int? EmployeeId { get; set; }

    public DateOnly? AccessFrom { get; set; }

    public DateOnly? AccessTo { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual ICollection<Ipaddress> Ipaddresses { get; set; } = new List<Ipaddress>();
}
