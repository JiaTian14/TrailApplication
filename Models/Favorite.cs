﻿using System;
using System.Collections.Generic;

namespace WebApplication2.Models;

public partial class Favorite
{
    public int FavoriteId { get; set; }

    public int UserId { get; set; }

    public int TrailId { get; set; }

    public virtual Trail Trail { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
