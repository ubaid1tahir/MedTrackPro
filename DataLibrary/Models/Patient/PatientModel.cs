
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models.Patient;

public class PatientModel
{
    [Key]
    public int PatientId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }

    public int Age {  get; set; }
    public string? GuardenerName {  get; set; }

    public string PhoneNumber {  get; set; }

    public bool isMarried {  get; set; }

    public decimal? Weight {  get; set; }

    public decimal? Height { get; set; }

    public string? AdditionalNote {  get; set; }
    public enum Gender;

    public int Severity {  get; set; } // 1-10

    public string UserId {  get; set; }

    public IdentityUser User { get; set; }
}

public enum Gender
{
    Male = 1, Female = 2
}

