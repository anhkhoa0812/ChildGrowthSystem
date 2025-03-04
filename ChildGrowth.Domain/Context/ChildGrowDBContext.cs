using System;
using System.Collections.Generic;
using ChildGrowth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ChildGrowth.Domain.Context;

public partial class ChildGrowDBContext : DbContext
{
    public ChildGrowDBContext()
    {
    }

    public ChildGrowDBContext(DbContextOptions<ChildGrowDBContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Blog> Blogs { get; set; }

    public virtual DbSet<Child> Children { get; set; }

    public virtual DbSet<Consultation> Consultations { get; set; }

    public virtual DbSet<FeedingRecord> FeedingRecords { get; set; }

    public virtual DbSet<GrowthAlert> GrowthAlerts { get; set; }

    public virtual DbSet<GrowthRecord> GrowthRecords { get; set; }

    public virtual DbSet<HealthEvent> HealthEvents { get; set; }

    public virtual DbSet<Immunization> Immunizations { get; set; }

    public virtual DbSet<MembershipPlan> MembershipPlans { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<StandardGrowthMetric> StandardGrowthMetrics { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAccount> UserAccounts { get; set; }

    public virtual DbSet<UserMembership> UserMemberships { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=160.191.244.31,1433;Database=ChildGrowthDB;User Id=sa;Password=123456aA@$;TrustServerCertificate=True;Encrypt=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Blog>(entity =>
        {
            entity.HasKey(e => e.BlogId).HasName("PK__Blogs__54379E50D1E54002");

            entity.Property(e => e.BlogId).HasColumnName("BlogID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.Category).HasMaxLength(50);
            entity.Property(e => e.CommentCount).HasDefaultValue(0);
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(255)
                .HasColumnName("ImageURL");
            entity.Property(e => e.LikeCount).HasDefaultValue(0);
            entity.Property(e => e.MetaDescription).HasMaxLength(500);
            entity.Property(e => e.PublishDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.Tags).HasMaxLength(255);
            entity.Property(e => e.Title).HasMaxLength(200);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.ViewCount).HasDefaultValue(0);

            entity.HasOne(d => d.Author).WithMany(p => p.Blogs)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Blogs__AuthorID__3B75D760");
        });

        modelBuilder.Entity<Child>(entity =>
        {
            entity.HasKey(e => e.ChildId).HasName("PK__Children__BEFA0736A4FEE9E7");

            entity.Property(e => e.ChildId).HasColumnName("ChildID");
            entity.Property(e => e.AllergiesNotes).HasMaxLength(500);
            entity.Property(e => e.BirthHeight).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.BirthWeight).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.BloodType).HasMaxLength(10);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DevelopmentalNotes).HasMaxLength(1000);
            entity.Property(e => e.EmergencyContact).HasMaxLength(255);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.InsuranceInfo).HasMaxLength(255);
            entity.Property(e => e.MedicalHistory).HasMaxLength(1000);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.PediaticianInfo).HasMaxLength(255);
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(255)
                .HasColumnName("PhotoURL");
            entity.Property(e => e.PreexistingConditions).HasMaxLength(500);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Parent).WithMany(p => p.Children)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Children__Parent__25869641");
        });

        modelBuilder.Entity<Consultation>(entity =>
        {
            entity.HasKey(e => e.ConsultationId).HasName("PK__Consulta__5D014A780CD5DDC0");

            entity.Property(e => e.ConsultationId).HasColumnName("ConsultationID");
            entity.Property(e => e.ChildId).HasColumnName("ChildID");
            entity.Property(e => e.ConsultationType).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.DoctorId).HasColumnName("DoctorID");
            entity.Property(e => e.Feedback).HasMaxLength(500);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.Priority).HasMaxLength(20);
            entity.Property(e => e.RequestDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ResponseDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);

            entity.HasOne(d => d.Child).WithMany(p => p.Consultations)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK__Consultat__Child__34C8D9D1");

            entity.HasOne(d => d.Doctor).WithMany(p => p.ConsultationDoctors)
                .HasForeignKey(d => d.DoctorId)
                .HasConstraintName("FK__Consultat__Docto__33D4B598");

            entity.HasOne(d => d.Parent).WithMany(p => p.ConsultationParents)
                .HasForeignKey(d => d.ParentId)
                .HasConstraintName("FK__Consultat__Paren__32E0915F");
        });

        modelBuilder.Entity<FeedingRecord>(entity =>
        {
            entity.HasKey(e => e.FeedingId).HasName("PK__FeedingR__F214E6DF9EBE123F");

            entity.Property(e => e.FeedingId).HasColumnName("FeedingID");
            entity.Property(e => e.Amount).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.ChildId).HasColumnName("ChildID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.FeedingDate).HasColumnType("datetime");
            entity.Property(e => e.FeedingType).HasMaxLength(50);
            entity.Property(e => e.Notes).HasMaxLength(500);

            entity.HasOne(d => d.Child).WithMany(p => p.FeedingRecords)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK__FeedingRe__Child__4CA06362");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.FeedingRecords)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK__FeedingRe__Creat__4D94879B");
        });

        modelBuilder.Entity<GrowthAlert>(entity =>
        {
            entity.HasKey(e => e.AlertId).HasName("PK__GrowthAl__EBB16AED82A5C447");

            entity.Property(e => e.AlertId).HasColumnName("AlertID");
            entity.Property(e => e.AlertLevel).HasMaxLength(20);
            entity.Property(e => e.AlertType).HasMaxLength(50);
            entity.Property(e => e.ChildId).HasColumnName("ChildID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.HandledAt).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.RecommendedActions).HasMaxLength(1000);

            entity.HasOne(d => d.Child).WithMany(p => p.GrowthAlerts)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK__GrowthAle__Child__2E1BDC42");

            entity.HasOne(d => d.HandledByNavigation).WithMany(p => p.GrowthAlerts)
                .HasForeignKey(d => d.HandledBy)
                .HasConstraintName("FK__GrowthAle__Handl__2F10007B");
        });

        modelBuilder.Entity<GrowthRecord>(entity =>
        {
            entity.HasKey(e => e.RecordId).HasName("PK__GrowthRe__FBDF78C9B2F1B7CC");

            entity.Property(e => e.RecordId).HasColumnName("RecordID");
            entity.Property(e => e.ActivityLevel).HasMaxLength(50);
            entity.Property(e => e.Bmi)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("BMI");
            entity.Property(e => e.ChildId).HasColumnName("ChildID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DevelopmentalMilestones).HasMaxLength(500);
            entity.Property(e => e.EatingHabits).HasMaxLength(500);
            entity.Property(e => e.HeadCircumference).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Height).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.PercentileBmi).HasColumnName("PercentileBMI");
            entity.Property(e => e.SleepPatterns).HasMaxLength(255);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.Weight).HasColumnType("decimal(5, 2)");

            entity.HasOne(d => d.Child).WithMany(p => p.GrowthRecords)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK__GrowthRec__Child__29572725");

            entity.HasOne(d => d.RecordedByNavigation).WithMany(p => p.GrowthRecords)
                .HasForeignKey(d => d.RecordedBy)
                .HasConstraintName("FK__GrowthRec__Recor__2A4B4B5E");
        });

        modelBuilder.Entity<HealthEvent>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__HealthEv__7944C8704475E24C");

            entity.Property(e => e.EventId).HasColumnName("EventID");
            entity.Property(e => e.ChildId).HasColumnName("ChildID");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.Doctor).HasMaxLength(100);
            entity.Property(e => e.EventType).HasMaxLength(50);
            entity.Property(e => e.Hospital).HasMaxLength(255);
            entity.Property(e => e.Medications).HasMaxLength(500);
            entity.Property(e => e.Severity).HasMaxLength(20);
            entity.Property(e => e.Treatment).HasMaxLength(500);

            entity.HasOne(d => d.Child).WithMany(p => p.HealthEvents)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK__HealthEve__Child__440B1D61");
        });

        modelBuilder.Entity<Immunization>(entity =>
        {
            entity.HasKey(e => e.ImmunizationId).HasName("PK__Immuniza__FAB9BD586FD31891");

            entity.Property(e => e.ImmunizationId).HasColumnName("ImmunizationID");
            entity.Property(e => e.BatchNumber).HasMaxLength(50);
            entity.Property(e => e.ChildId).HasColumnName("ChildID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.GivenBy).HasMaxLength(100);
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Notes).HasMaxLength(500);
            entity.Property(e => e.VaccineName).HasMaxLength(100);

            entity.HasOne(d => d.Child).WithMany(p => p.Immunizations)
                .HasForeignKey(d => d.ChildId)
                .HasConstraintName("FK__Immunizat__Child__412EB0B6");
        });

        modelBuilder.Entity<MembershipPlan>(entity =>
        {
            entity.HasKey(e => e.PlanId).HasName("PK__Membersh__755C22D78F51BDC4");

            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.AnnualDiscount).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(1000);
            entity.Property(e => e.DiscountPercentage).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Features).HasMaxLength(1000);
            entity.Property(e => e.PlanName).HasMaxLength(100);
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__20CF2E329A84DEB1");

            entity.Property(e => e.NotificationId).HasColumnName("NotificationID");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.IsRead).HasDefaultValue(false);
            entity.Property(e => e.Message).HasMaxLength(500);
            entity.Property(e => e.Priority).HasMaxLength(20);
            entity.Property(e => e.ReadAt).HasColumnType("datetime");
            entity.Property(e => e.RelatedId).HasColumnName("RelatedID");
            entity.Property(e => e.Title).HasMaxLength(100);
            entity.Property(e => e.Type).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Notificat__UserI__48CFD27E");
        });

        modelBuilder.Entity<StandardGrowthMetric>(entity =>
        {
            entity.HasKey(e => e.MetricId).HasName("PK__Standard__56105645AC97A42C");

            entity.Property(e => e.MetricId).HasColumnName("MetricID");
            entity.Property(e => e.AgeGroup).HasMaxLength(50);
            entity.Property(e => e.Bmimax)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("BMIMax");
            entity.Property(e => e.Bmimin)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("BMIMin");
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.HeightMax).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.HeightMin).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.Source).HasMaxLength(50);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
            entity.Property(e => e.WeightMax).HasColumnType("decimal(5, 2)");
            entity.Property(e => e.WeightMin).HasColumnType("decimal(5, 2)");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCAC8FFEEFB6");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4CFAF7445").IsUnique();

            entity.HasIndex(e => e.Email, "UQ__Users__A9D10534DD8D3CBE").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.AvatarUrl)
                .HasMaxLength(255)
                .HasColumnName("AvatarURL");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.Gender).HasMaxLength(10);
            entity.Property(e => e.Language).HasMaxLength(20);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.LastNotificationCheck).HasColumnType("datetime");
            entity.Property(e => e.MembershipStatus)
                .HasMaxLength(20)
                .HasDefaultValue("Free");
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TimeZone).HasMaxLength(50);
            entity.Property(e => e.UserType).HasMaxLength(20);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UserAccount>(entity =>
        {
            entity.ToTable("UserAccount");

            entity.Property(e => e.UserAccountId).HasColumnName("UserAccountID");
            entity.Property(e => e.ApplicationCode).HasMaxLength(50);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.EmployeeCode).HasMaxLength(50);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.ModifiedBy).HasMaxLength(50);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.Phone).HasMaxLength(50);
            entity.Property(e => e.RequestCode).HasMaxLength(50);
            entity.Property(e => e.UserName).HasMaxLength(50);
        });

        modelBuilder.Entity<UserMembership>(entity =>
        {
            entity.HasKey(e => e.MembershipId).HasName("PK__UserMemb__92A785997DF7499E");

            entity.Property(e => e.MembershipId).HasColumnName("MembershipID");
            entity.Property(e => e.CancellationReason).HasMaxLength(255);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.PaymentMethod).HasMaxLength(50);
            entity.Property(e => e.PaymentStatus).HasMaxLength(20);
            entity.Property(e => e.PlanId).HasColumnName("PlanID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(20);
            entity.Property(e => e.TransactionId)
                .HasMaxLength(100)
                .HasColumnName("TransactionID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Plan).WithMany(p => p.UserMemberships)
                .HasForeignKey(d => d.PlanId)
                .HasConstraintName("FK__UserMembe__PlanI__20C1E124");

            entity.HasOne(d => d.User).WithMany(p => p.UserMemberships)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__UserMembe__UserI__1FCDBCEB");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
