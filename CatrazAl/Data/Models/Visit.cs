using System;
using System.Collections.Generic;

namespace CatrazAl.Data.Models;

public partial class Visit
{
    public int VisitId { get; set; }

    public int PrisonerId { get; set; }

    public string VisitorFirstName { get; set; } = null!;

    public string VisitorLastName { get; set; } = null!;

    public string VisitorRelation { get; set; } = null!;

    public DateTime VisitDate { get; set; }

    public int DurationMinuits { get; set; }

    public virtual Prisoner Prisoner { get; set; } = null!;
}
