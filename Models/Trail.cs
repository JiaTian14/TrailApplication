using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Trail
{
    public int TrailId { get; set; }

    public string? TrailName { get; set; }

    public string? TrailLevel { get; set; }

    public string? Distance { get; set; }

    public string? Description { get; set; }

    public double? TotalDistance { get; set; }

    public double? Distances { get; set; }

    public double? TotalDuration { get; set; }

    public int? TotalActivities { get; set; }

    public virtual ICollection<Activity> Activities { get; set; } = new List<Activity>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();
}
