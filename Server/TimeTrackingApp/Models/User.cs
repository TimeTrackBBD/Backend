using System;
using System.Collections.Generic;

namespace TimeTrackingApp.Models;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
}
