using System;
using System.Collections.Generic;

namespace CatrazAl.Data.Models;

public partial class Guard
{
    public int GuardId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? GuardRank { get; set; }

    public string? Phone { get; set; }

    public int ShiftId { get; set; }

    public virtual Shift Shift { get; set; } = null!;
}
