using System;
using System.Collections.Generic;

namespace FBOX.Entities;

public partial class Club
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Team> Teams { get; } = new List<Team>();
}
