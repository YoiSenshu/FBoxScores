using System;
using System.Collections.Generic;

namespace FBOX.Entities;

public partial class Team
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public int ClubId { get; set; }

    public virtual Club Club { get; set; } = null!;

    public virtual ICollection<Playerteam> Playerteams { get; } = new List<Playerteam>();
}
