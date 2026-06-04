using System;
using System.Collections.Generic;

namespace CatrazAl.Data.Models;

public partial class PrisonBlock
{
    public int PrisonBlockId { get; set; }

    public string? PrisonBlock1 { get; set; }

    public virtual ICollection<Cell> Cells { get; set; } = new List<Cell>();

    public virtual ICollection<Prisoner> Prisoners { get; set; } = new List<Prisoner>();

    public virtual ICollection<Shift> Shifts { get; set; } = new List<Shift>();
}
