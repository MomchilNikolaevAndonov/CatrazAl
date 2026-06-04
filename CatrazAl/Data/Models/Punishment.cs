using System;
using System.Collections.Generic;

namespace CatrazAl.Data.Models;

public partial class Punishment
{
    public int PunishmentId { get; set; }

    public int PrisonerId { get; set; }

    public string Reason { get; set; } = null!;

    public int PunishmentDays { get; set; }

    public DateTime StartDate { get; set; }

    public DateTime EndDate { get; set; }

    public string? PunishmentType { get; set; }

    public virtual Prisoner Prisoner { get; set; } = null!;
}
