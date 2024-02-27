using System;
using System.Collections.Generic;

namespace FBOX.Entities;

public partial class Sensorsconfig
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Value { get; set; }

    public int WallNr { get; set; }
}
