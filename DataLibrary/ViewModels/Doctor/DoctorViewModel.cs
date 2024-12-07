
using DataLibrary.Models.Doctor;
using Microsoft.AspNetCore.Http;


namespace DataLibrary.ViewModels.Doctor;

public class DoctorViewModel
{
    public int DoctorId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public string EmailAddress { get; set; }
    public string? PhoneNumber { get; set; }

    public string LicenseNumber { get; set; }

    public string? Qualifications { get; set; }
    public IFormFile? Photo { get; set; }
    public string? LongDescription { get; set; }

    public string? ShortDescription { get; set; }

    public int? YearsOfExperience { get; set; }

    public string Status { get; set; }

    public string? WorkingHours { get; set; }

    public string? EmergencyContact { get; set; }

    public string Rank { get; set; }

    public string? AddedById { get; set; }

    public string Password {  get; set; }

    public string Category {  get; set; }

    public List<DoctorCategory> DoctorCategories { get; set; } = new List<DoctorCategory>();
}
