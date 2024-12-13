using DataLibrary.Models.DoctorNamespace;
using DataLibrary.Models.Patient;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MedTrackPro.Data;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
        
    }

    public DbSet<Message> Messages { get; set; }

    public DbSet<PatientModel> Patients { get; set; }

    public DbSet<DoctorCategory> DoctorCategories { get; set; }

    public DbSet<Doctor> Doctors { get; set; }

    public DbSet<DoctorQualification> DoctorQualifications { get; set; }

    public DbSet<DoctorExperience> DoctorExperiences { get; set; }

    public DbSet<DoctorCertification> DoctorCertifications { get; set; }

    public DbSet<DoctorMembership> DoctorMemberships { get; set; }

    public DbSet<Award> Awards { get; set; }

    public DbSet<Medication> Medications { get; set; }

    public DbSet<MedicalHistory> MedicalHistories { get; set; }

    public DbSet<Appointment> Appointments { get; set; }

    public DbSet<LabResult> LabResults { get; set; }

    public DbSet<Vaccination> Vaccinations { get; set; }

    public DbSet<VitalSign> VitalSigns { get; set; }

    public DbSet<Immunization> Immunizations { get; set; }

    public DbSet<Document> Documents { get; set; }

    public DbSet<Note> Notes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Message>()
            .HasOne(m => m.Receiver)
            .WithMany()
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Message>()
            .HasOne(m => m.Sender)
            .WithMany()
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<PatientModel>()
            .HasOne(p => p.User)
            .WithOne()
            .HasForeignKey<PatientModel>(m => m.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Doctor>()
            .HasOne(d => d.DoctorCategory)
            .WithMany(c => c.Doctors)
            .HasForeignKey(d => d.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Award>()
            .HasOne(d => d.Doctor)
            .WithMany()
            .HasForeignKey(d => d.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<DoctorMembership>()
            .HasOne(d => d.Doctor)
            .WithMany()
            .HasForeignKey(d => d.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<DoctorCertification>()
            .HasOne(d => d.Doctor)
            .WithMany()
            .HasForeignKey(d => d.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<DoctorExperience>()
            .HasOne(d => d.Doctor)
            .WithMany()
            .HasForeignKey(d => d.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<DoctorQualification>()
            .HasOne(d => d.Doctor)
            .WithMany()
            .HasForeignKey(d => d.DoctorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Note>()
            .HasOne(d => d.Patient)
            .WithMany()
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Note>()
            .HasOne(p => p.Author)
            .WithMany()
            .HasForeignKey(p => p.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<Document>()
            .HasOne(p => p.Patient)
            .WithMany()
            .HasForeignKey(p=>p.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Immunization>()
            .HasOne(p => p.Patient)
            .WithMany()
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<VitalSign>()
            .HasOne(p => p.Patient)
            .WithMany()
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Vaccination>()
            .HasOne(p => p.Patient)
            .WithMany()
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<LabResult>()
            .HasOne(p => p.Patient)
            .WithMany()
            .HasForeignKey(p => p.PatientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Appointment>()
           .HasOne(p => p.Patient)
           .WithMany()
           .HasForeignKey(p => p.PatientId)
           .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Appointment>()
          .HasOne(p => p.Doctor)
          .WithMany()
          .HasForeignKey(p => p.DoctorId)
          .OnDelete(DeleteBehavior.NoAction);

        builder.Entity<MedicalHistory>()
          .HasOne(p => p.Patient)
          .WithMany()
          .HasForeignKey(p => p.PatientId)
          .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Medication>()
          .HasOne(p => p.Patient)
          .WithMany()
          .HasForeignKey(p => p.PatientId)
          .OnDelete(DeleteBehavior.Cascade);
    }
}
