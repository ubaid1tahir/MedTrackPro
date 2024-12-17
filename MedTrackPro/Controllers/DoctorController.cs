using DataLibrary.Models.DoctorNamespace;
using DataLibrary.ViewModels.Doctor;
using MedTrackPro.Data;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MedTrackPro.Controllers;

public class DoctorController : Controller
{
    private readonly ApplicationDbContext _context;

    private readonly IWebHostEnvironment _environment;
    public DoctorController(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }
    public IActionResult Profile(string id)
    {
        var doctor = _context.Doctors.Where(d => d.UserId == id).FirstOrDefault();
        if (doctor != null)
        {
            var viewModel = new DoctorViewModel
            {
                DoctorId = doctor.DoctorId,
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
        var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var doctor = _context.Doctors.Where(d => d.UserId == doctorId).FirstOrDefault();
        if(doctor == null)
        {
            return RedirectToAction("NotFound", "Error");
        }
        var viewModel = new DoctorViewModel();
        viewModel.FirstName = doctor.FirstName;
        viewModel.LastName = doctor.LastName;
        viewModel.EmergencyContact = doctor?.EmergencyContact;
        viewModel.State = doctor?.State;
        viewModel.Country = doctor?.Country;
        viewModel.PhoneNumber = doctor?.PhoneNumber;
        viewModel.EmergencyContact = doctor?.EmergencyContact;
        viewModel.ShortDescription = doctor?.ShortDescription;
        viewModel.LongDescription = doctor?.LongDescription;
        viewModel.YearsOfExperience = doctor?.YearsOfExperience;
        viewModel.PhotoUrl = doctor?.Photo;
        return View(viewModel);
    }

    [HttpPost]
    public async Task<IActionResult> CompleteProfile(DoctorViewModel model)
    {
        var doctorId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var folderPath = Path.Combine(_environment.WebRootPath, "uploads");
        var directory = Directory.CreateDirectory(folderPath);
        if (directory.Exists)
        {
            if(model.Photo != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(model.Photo.FileName);
                var filePath = Path.Combine(folderPath, fileName);
                using(var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model?.Photo.CopyToAsync(stream);
                }
                model.PhotoUrl = "/uploads/" + fileName;
            }
        }
        var doctor = _context.Doctors.Where(d => d.UserId == doctorId).FirstOrDefault();
        if(doctor == null)
        {
            Console.WriteLine($"{DateTime.Now} :- Doctor Not Found in Complete Profile Post Method");
        }

        doctor.LongDescription = model?.LongDescription;
        doctor.ShortDescription = model?.ShortDescription;
        doctor.YearsOfExperience = model?.YearsOfExperience;
        doctor.EmergencyContact = model?.EmergencyContact;
        doctor.PhoneNumber = model?.PhoneNumber;
        doctor.WorkingHours = model?.WorkingHours;
        doctor.DaysAvailable = model?.DaysAvailable;
        doctor.WorkingDays = model?.WorkingDays;
        doctor.Country = model?.Country;
        doctor.Photo = model?.PhotoUrl;
        doctor.State = model?.State;
        
        _context.Doctors.Update(doctor);
        await _context.SaveChangesAsync();

        return RedirectToAction("Profile", new { id = doctorId });
    }
}
