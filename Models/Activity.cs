using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Activity
{
    public int ActivityId { get; set; }

    public int TrailId { get; set; }

    public string? ActivityType { get; set; }

    public string? ActivityTime { get; set; }

    public string? ActivityDate { get; set; }

    public virtual Trail Trail { get; set; } = null!;
}
