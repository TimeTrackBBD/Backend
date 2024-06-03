using System;
using System.Collections.Generic;

namespace TimeTrackingApp.Models;

public partial class Project
{
    public int ProjectId { get; set; }

    public int UserId { get; set; }

    public string ProjectName { get; set; } = null!;

    public string Description { get; set; } = null!;

}
