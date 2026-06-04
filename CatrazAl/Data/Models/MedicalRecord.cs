using System;
using System.Collections.Generic;

namespace CatrazAl.Data.Models;

public partial class MedicalRecord
{
    public int RecordId { get; set; }

    public int PrisonerId { get; set; }

    public string Diagnosis { get; set; } = null!;

    public string? Treatment { get; set; }

    public int TreatmentDays { get; set; }

    public string? DoctorFirstName { get; set; }

    public string? DoctorLastName { get; set; }

    public DateTime RecordDate { get; set; }

    public virtual Prisoner Prisoner { get; set; } = null!;
}
