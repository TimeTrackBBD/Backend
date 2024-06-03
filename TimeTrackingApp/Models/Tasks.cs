using System;
using System.Collections.Generic;

namespace TimeTrackingApp.Models;

public partial class Tasks
{
    public int TaskId { get; set; }

    public int ProjectId { get; set; }

    public int PriorityId { get; set; }

    public string TaskName { get; set; } = null!;

    public string Description { get; set; } = null!;

    public int? Duration { get; set; }
}
