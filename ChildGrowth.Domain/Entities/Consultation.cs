using System;
using System.Collections.Generic;

namespace ChildGrowth.Domain.Entities;

public partial class Consultation
{
    public int ConsultationId { get; set; }

    public int? ParentId { get; set; }

    public int? DoctorId { get; set; }

    public int? ChildId { get; set; }

    public DateTime? RequestDate { get; set; }

    public string? ConsultationType { get; set; }

    public string? Description { get; set; }

    public string? SharedData { get; set; }

    public string? Status { get; set; }

    public string? Response { get; set; }

    public DateTime? ResponseDate { get; set; }

    public int? Rating { get; set; }

    public string? Feedback { get; set; }

    public DateTime? FollowUpDate { get; set; }

    public string? Priority { get; set; }

    public virtual Child? Child { get; set; }

    public virtual User? Doctor { get; set; }

    public virtual User? Parent { get; set; }
}
