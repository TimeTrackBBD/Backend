﻿using System;
using System.Collections.Generic;

namespace TimeTrackingApp.Models;

public partial class User
{
    public int UserId { get; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

}
