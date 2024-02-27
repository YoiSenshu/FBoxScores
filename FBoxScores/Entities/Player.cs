using System;
using System.Collections.Generic;

namespace FBOX.Entities;

public partial class Player
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public virtual ICollection<Exercisereport> Exercisereports { get; set; } = new List<Exercisereport>();

    public virtual ICollection<Playerposition> Playerpositions { get; set; } = new List<Playerposition>();

    public virtual ICollection<Playerteam> Playerteams { get; set; } = new List<Playerteam>();
    public virtual List<GameRecordPlayer> Scores { get; set; } = new List<GameRecordPlayer>();

    public override string ToString()
    {
        return this.Name + " " + this.Surname;
    }
}
