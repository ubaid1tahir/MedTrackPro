
using DataLibrary.Models.DoctorNamespace;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;


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
    public string? PhotoUrl {  get; set; }
    public string? LongDescription { get; set; }

    public string? ShortDescription { get; set; }

    public int? YearsOfExperience { get; set; }

    public string Status { get; set; }

    public string? WorkingHours { get; set; }

    public string? EmergencyContact { get; set; }

    public string Rank { get; set; }

    public string? AddedById { get; set; }

    public string Password {  get; set; }

    public string? LanguagesSpoken { get; set; }
    public string? WorkingDays { get; set; } // Mon - Thur 8:00 - 12:00 

    public string? DaysAvailable { get; set; }

    public DateOnly? LicenseIssueDate { get; set; }

    public DateOnly? LicenseExpiryDate { get; set; }

    public string? UserId {  get; set; }

    public string State { get; set; }

    public string? Country {  get; set; }

    public int? Reviews { get; set; }
    public string Category {  get; set; }
    // To add a doctor, we display categories and we use the below field
    public List<DoctorCategory> DoctorCategories { get; set; } = new List<DoctorCategory>();
}


public class DoctorQualificationViewModel
{
    public int QualificationId { get; set; }

    public string Degree { get; set; }
    public string Institute { get; set; }
    public int YearOfCompletion { get; set; }
    public string Country { get; set; }

    public int DoctorId { get; set; }
}

public class DoctorExperienceViewModel
{
    public int ExperienceId { get; set; }
    public string Hospital { get; set; }
    public string Position { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    public string Location { get; set; }

    public int DoctorId { get; set; }
}

public class DoctorCertificationViewModel
{
    public int CertificationId { get; set; }
    public string Name { get; set; }
    public string Organization { get; set; }
    public DateOnly IssueDate { get; set; }
    public DateOnly? ExpiryDate { get; set; }

    public int DoctorId { get; set; }
}

public class DoctorMembershipViewModel
{
    public int MembershipId { get; set; }
    public string Role { get; set; }
    public string Organization { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }

    public int DoctorId { get; set; }
}

public class AwardViewModel
{
    public int AwardId { get; set; }

    public string Title { get; set; }

    public string Organization { get; set; }

    public int Year { get; set; }

    public int DoctorId { get; set; }
}





public class DoctorCategoryViewModel
{
    public int CategoryId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }

    public bool? isActive { get; set; }
}