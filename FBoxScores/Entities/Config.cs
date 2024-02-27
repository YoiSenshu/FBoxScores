using System;
using System.Collections.Generic;

namespace FBOX.Entities;

public partial class Config
{
    public int Id { get; set; }

    public int ExerciseId { get; set; }

    public string? GeneralConfig { get; set; }

    public string? ScenarioConfig { get; set; }

    public string? Name { get; set; }

    public DateTime? Date { get; set; }

    public bool IsAutosave { get; set; }
}
