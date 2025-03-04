using System;
using System.Collections.Generic;

namespace ChildGrowth.Domain.Entities;

public partial class Immunization
{
    public int ImmunizationId { get; set; }

    public int? ChildId { get; set; }

    public string? VaccineName { get; set; }

    public DateOnly? DateGiven { get; set; }

    public int? DoseNumber { get; set; }

    public DateOnly? NextDueDate { get; set; }

    public string? GivenBy { get; set; }

    public string? Location { get; set; }

    public string? BatchNumber { get; set; }

    public string? Notes { get; set; }

    public bool? RemindStatus { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Child? Child { get; set; }
}
