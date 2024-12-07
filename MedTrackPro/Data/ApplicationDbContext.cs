using DataLibrary.Models.Doctor;
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
    }
}
