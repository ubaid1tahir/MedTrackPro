using DataLibrary.Models.Doctor;
using DataLibrary.Models.Patient;
using DataLibrary.ViewModels.Doctor;
using DataLibrary.ViewModels.Patient;
using MedTrackPro.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace MedTrackPro.Controllers;

//[Authorize(Roles = "admin,patient")]
public class PatientController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ApplicationDbContext _context;

    public PatientController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    public async Task<IActionResult> Message(string id)
    {
        if(id != null)
        {
            var user = await _userManager.FindByIdAsync(id);
            var senderId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(user != null)
            {
                var contact = new Contact
                {
                    Id = id,
                    Name = user.UserName
                };
                var chat = _context.Messages.FromSqlInterpolated
                    ($"exec dbo.spMessage_GetMessageByIds @SenderId = {senderId}, @ReceiverId = {id};")
                    .AsEnumerable().Select(message => new Message
                    {
                        Id=message.Id,
                        ReceiverId=message.ReceiverId,
                        SenderId=message.SenderId,
                        Text=message.Text,
                    }).ToList();

                var viewModel = new MessageViewModel
                {
                    contact = contact,
                    messages = chat
                };
                return View(viewModel);
            }
        }
        return NotFound();
    }

    public IActionResult Contacts()
    {
        var users = _userManager.Users.ToList();
        List<Contact> contacts = new List<Contact>();
        var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        foreach (var user in users)
        {
            if(user.Id != currentUserId)
            {
                contacts.Add(new Contact
                {
                    Id = user.Id,
                    Name = user.UserName
                });
            }
            
        }
        return View(contacts);
    }

    public IActionResult SelectDoctor()
    {
        var doctors = _context.Doctors.FromSqlInterpolated($"exec dbo.spDoctor_GetAllDoctorsOnExperience")
            .AsEnumerable().Select(doctor => new Doctor
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Rank = doctor?.Rank,
                Status = doctor.Status,
                ShortDescription = doctor?.ShortDescription,
                YearsOfExperience = doctor?.YearsOfExperience,
                Photo = doctor?.Photo
            }).ToList();

        var categories = _context.DoctorCategories.ToList();

        var categoryViewModelList = new List<DoctorCategoryViewModel>();
        foreach(var category in categories)
        {
            categoryViewModelList.Add(new DoctorCategoryViewModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
            });
        }

        var doctorViewModelList = new List<DoctorViewModel>();
        foreach (var doctor in doctors)
        {
            doctorViewModelList.Add(new DoctorViewModel
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Rank = doctor?.Rank,
                Status = doctor.Status,
                ShortDescription = doctor?.ShortDescription,
                YearsOfExperience = doctor?.YearsOfExperience,
                PhotoUrl = doctor?.Photo
            });
        }
        var viewModel = new SelectDoctorViewModel
        {
            doctorCategories = categoryViewModelList,
            Doctors = doctorViewModelList
        };
        return View(viewModel);
    }

    public IActionResult FindDoctorsByCategory(int id)
    {
        var doctors = _context.Doctors.FromSqlInterpolated($"exec dbo.spDoctor_GetByCategory @CategoryId={id}")
            .AsEnumerable().Select(doctor => new Doctor
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Rank = doctor?.Rank,
                Status = doctor.Status,
                ShortDescription = doctor?.ShortDescription,
                YearsOfExperience = doctor?.YearsOfExperience,
                Photo = doctor?.Photo
            }).ToList();

        var doctorViewModelList = new List<DoctorViewModel>();
        foreach (var doctor in doctors)
        {
            doctorViewModelList.Add(new DoctorViewModel
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Rank = doctor?.Rank,
                Status = doctor.Status,
                ShortDescription = doctor?.ShortDescription,
                YearsOfExperience = doctor?.YearsOfExperience,
                PhotoUrl = doctor?.Photo
            });
        }
        var viewModel = new SelectDoctorViewModel
        {
            Doctors = doctorViewModelList
        };
        return View(viewModel);
    }
}
