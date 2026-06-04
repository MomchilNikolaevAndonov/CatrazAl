using System;
using System.Collections.Generic;

namespace CatrazAl.Data.Models;

public partial class Cell
{
    public int CellId { get; set; }

    public int PrisonBlockId { get; set; }

    public int? Capacity { get; set; }

    public string? Kind { get; set; }

    public virtual PrisonBlock PrisonBlock { get; set; } = null!;

    public virtual ICollection<Prisoner> Prisoners { get; set; } = new List<Prisoner>();
}
