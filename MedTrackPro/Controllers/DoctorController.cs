using DataLibrary.Models.DoctorNamespace;
using DataLibrary.ViewModels.Doctor;
using MedTrackPro.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedTrackPro.Controllers;

public class DoctorController : Controller
{
    private readonly ApplicationDbContext _context;
    public DoctorController(ApplicationDbContext context)
    {
        _context = context;
    }
    public IActionResult Profile(int id)
    {
        var doctor = _context.Doctors.Where(d => d.DoctorId == id).FirstOrDefault();
        if (doctor != null)
        {
            var viewModel = new DoctorViewModel
            {
                DoctorId = id,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                EmailAddress = doctor.EmailAddress,
                LicenseNumber = doctor.LicenseNumber,
                EmergencyContact = doctor?.EmergencyContact,
                PhotoUrl = doctor?.Photo,
                Qualifications = doctor?.Qualifications,
                Rank = doctor?.Rank,
                Status = doctor?.Status,
                ShortDescription = doctor?.ShortDescription,
                LongDescription = doctor?.LongDescription,
                YearsOfExperience = doctor?.YearsOfExperience,
                WorkingHours = doctor?.WorkingHours,
                PhoneNumber = doctor?.PhoneNumber,
            };
            return View(viewModel);
        }
        return RedirectToAction("NotFound", "Error");
    }
    [HttpGet]
    public IActionResult CompleteProfile()
    {
        var viewModel = new DoctorViewModel();
        return View(viewModel);
    }

    [HttpPost]
    public IActionResult CompleteProfile(DoctorViewModel model)
    {
        var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var doctor = new Doctor()
        {
            LongDescription = model?.LongDescription,
            ShortDescription = model?.ShortDescription,
            YearsOfExperience = model?.YearsOfExperience,
            EmergencyContact = model?.EmergencyContact,
            PhoneNumber = model?.PhoneNumber,
            WorkingHours = model?.WorkingHours
        };

        return View();
    }
}
