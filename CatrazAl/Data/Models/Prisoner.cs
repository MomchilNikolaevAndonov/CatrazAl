using System;
using System.Collections.Generic;

namespace CatrazAl.Data.Models;

public partial class Prisoner
{
    public int PrisonerId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Egn { get; set; } = null!;

    public DateOnly DateOfBirth { get; set; }

    public string Gender { get; set; } = null!;

    public int CrimeId { get; set; }

    public int SentenceMonths { get; set; }

    public DateOnly SentenceStart { get; set; }

    public DateOnly SentenceEnd { get; set; }

    public int CellId { get; set; }

    public bool Released { get; set; }

    public int PrisonBlockId { get; set; }

    public virtual Cell Cell { get; set; } = null!;

    public virtual Crime Crime { get; set; } = null!;

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual PrisonBlock PrisonBlock { get; set; } = null!;

    public virtual ICollection<Punishment> Punishments { get; set; } = new List<Punishment>();

    public virtual ICollection<Visit> Visits { get; set; } = new List<Visit>();
}
