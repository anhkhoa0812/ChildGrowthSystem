using System;
using System.Collections.Generic;

namespace ChildGrowth.Domain.Entities;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? Address { get; set; }

    public string? UserType { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? LastLogin { get; set; }

    public string? Status { get; set; }

    public string? AvatarUrl { get; set; }

    public string? MembershipStatus { get; set; }

    public string? NotificationPreferences { get; set; }

    public string? Language { get; set; }

    public string? TimeZone { get; set; }

    public DateTime? LastNotificationCheck { get; set; }

    public bool? TwoFactorEnabled { get; set; }

    public string? SecurityQuestions { get; set; }

    public virtual ICollection<Blog> Blogs { get; set; } = new List<Blog>();

    public virtual ICollection<Child> Children { get; set; } = new List<Child>();

    public virtual ICollection<Consultation> ConsultationDoctors { get; set; } = new List<Consultation>();

    public virtual ICollection<Consultation> ConsultationParents { get; set; } = new List<Consultation>();

    public virtual ICollection<FeedingRecord> FeedingRecords { get; set; } = new List<FeedingRecord>();

    public virtual ICollection<GrowthAlert> GrowthAlerts { get; set; } = new List<GrowthAlert>();

    public virtual ICollection<GrowthRecord> GrowthRecords { get; set; } = new List<GrowthRecord>();

    public virtual ICollection<Notification> Notifications { get; set; } = new List<Notification>();

    public virtual ICollection<UserMembership> UserMemberships { get; set; } = new List<UserMembership>();
}
