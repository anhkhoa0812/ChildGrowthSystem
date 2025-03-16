using System.ComponentModel.DataAnnotations;

namespace ChildGrowth.API.Payload.Request.Children;

    public class UpdateChildrenRequest
    {
    [MaxLength(100)]
    public string? FullName { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [RegularExpression("Male|Female|Other", ErrorMessage = "Gender must be Male, Female, or Other")]
    public string? Gender { get; set; }

    [MaxLength(10)]
    public string? BloodType { get; set; }

    public string? AllergiesNotes { get; set; }

    public string? MedicalHistory { get; set; }

    public decimal? BirthWeight { get; set; }

    public decimal? BirthHeight { get; set; }

    public string? PreexistingConditions { get; set; }

    public string? EmergencyContact { get; set; }

    public string? InsuranceInfo { get; set; }

    public string? PediatricianInfo { get; set; }

    public string? DevelopmentalNotes { get; set; }

    public string? PhotoUrl { get; set; }
}

