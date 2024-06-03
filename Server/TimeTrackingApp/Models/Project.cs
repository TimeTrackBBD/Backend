using System;
using System.Collections.Generic;

namespace TimeTrackingApp.Models;

public partial class Project
{
    public int ProjectId { get; set; }

    public int UserId { get; set; }

    public string ProjectName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public virtual ICollection<Tasks> Tasks { get; set; } = new List<Tasks>();

    public virtual User User { get; set; } = null!;
}
