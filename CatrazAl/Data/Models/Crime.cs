using System;
using System.Collections.Generic;

namespace CatrazAl.Data.Models;

public partial class Crime
{
    public int CrimeId { get; set; }

    public string? Crime1 { get; set; }

    public virtual ICollection<Prisoner> Prisoners { get; set; } = new List<Prisoner>();
}
