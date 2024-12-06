
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models.Doctor;

public class Doctor
{
    [Key]
    public int DoctorId { get; set; }
    public string FirstName {  get; set; }
    public string LastName { get; set; }
    public string Photo {  get; set; }
    public string LongDescription {  get; set; }

    public string ShortDescription { get; set; }

    public int YearsOfExperience {  get; set; }

    public string Rank {  get; set; }

    public string AddedById {  get; set; }
    public IdentityUser User { get; set; }

    public int CategoryId {  get; set; }

    public DoctorCategory DoctorCategory { get; set; }
}

public class DoctorCategory
{
    [Key]
    public int CategoryId { get; set; }

    public string Name { get; set; }
    public string Description { get; set; }
    
}