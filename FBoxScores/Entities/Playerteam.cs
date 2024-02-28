using System;
using System.Collections.Generic;

namespace FBox.Entities;

public partial class Playerteam
{
    public int Id { get; set; }

    public int PlayerId { get; set; }

    public int TeamId { get; set; }

    public virtual Player Player { get; set; } = null!;

    public virtual Team Team { get; set; } = null!;
}
