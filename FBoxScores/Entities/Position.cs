using System;
using System.Collections.Generic;

namespace FBox.Entities;

public partial class Position
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Playerposition> Playerpositions { get; } = new List<Playerposition>();
}
