using System;
using System.Collections.Generic;

namespace CatrazAl.Data.Models;

public partial class Shift
{
    public int ShiftId { get; set; }

    public string ShiftName { get; set; } = null!;

    public DateTime? StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public int PrisonBlockId { get; set; }

    public virtual ICollection<Guard> Guards { get; set; } = new List<Guard>();

    public virtual PrisonBlock PrisonBlock { get; set; } = null!;
}
