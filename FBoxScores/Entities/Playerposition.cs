using System;
using System.Collections.Generic;

namespace FBOX.Entities;

public partial class Playerposition
{
    public int Id { get; set; }

    public int PlayerId { get; set; }

    public int PositionId { get; set; }

    public virtual Player Player { get; set; } = null!;

    public virtual Position Position { get; set; } = null!;
}
