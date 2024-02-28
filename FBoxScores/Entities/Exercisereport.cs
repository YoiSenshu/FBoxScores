using System;
using System.Collections.Generic;

namespace FBox.Entities;

public partial class Exercisereport
{
    public int Id { get; set; }

    public int? ExerciseId { get; set; }

    public int? PlayerId { get; set; }

    public byte[]? ReportBinary { get; set; }

    public DateTime? Date { get; set; }

    public virtual Player? Player { get; set; }
}
