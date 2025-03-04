using System;
using System.Collections.Generic;

namespace ChildGrowth.Domain.Entities;

public partial class Child
{
    public int ChildId { get; set; }

    public int? ParentId { get; set; }

    public string? FullName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? BloodType { get; set; }

    public string? AllergiesNotes { get; set; }

    public string? MedicalHistory { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? Status { get; set; }

    public decimal? BirthWeight { get; set; }

    public decimal? BirthHeight { get; set; }

    public string? PreexistingConditions { get; set; }

    public string? EmergencyContact { get; set; }

    public string? InsuranceInfo { get; set; }

    public string? PediaticianInfo { get; set; }

    public string? DevelopmentalNotes { get; set; }

    public string? PhotoUrl { get; set; }

    public virtual ICollection<Consultation> Consultations { get; set; } = new List<Consultation>();

    public virtual ICollection<FeedingRecord> FeedingRecords { get; set; } = new List<FeedingRecord>();

    public virtual ICollection<GrowthAlert> GrowthAlerts { get; set; } = new List<GrowthAlert>();

    public virtual ICollection<GrowthRecord> GrowthRecords { get; set; } = new List<GrowthRecord>();

    public virtual ICollection<HealthEvent> HealthEvents { get; set; } = new List<HealthEvent>();

    public virtual ICollection<Immunization> Immunizations { get; set; } = new List<Immunization>();

    public virtual User? Parent { get; set; }
}
