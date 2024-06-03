using System;
using System.Collections.Generic;

namespace TimeTrackingApp.Models;

public partial class Priority
{
    public int PriorityId { get; set; }

    public string PriorityName { get; set; } = null!;

    public virtual ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();
}
