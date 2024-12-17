using DataLibrary.ViewModels.Doctor;
using DataLibrary.ViewModels.Patient;
using MedTrackPro.Data;
using MedTrackPro.UtilityMethods.Interfaces;
using Microsoft.EntityFrameworkCore;
using DataLibrary.Models.DoctorNamespace;
namespace MedTrackPro.UtilityMethods.Implementations;

public class FindDoctor : IDoctor
{
    public SelectDoctorViewModel FindDoctors(ApplicationDbContext _context, int? id = 0)
    {
        List<Doctor>? doctors = new List<Doctor>();
        if(id != 0)
        {
            doctors = _context.Doctors.FromSqlInterpolated($"exec dbo.spDoctor_GetByCategory @CategoryId={id}")
            .AsEnumerable().Select(doctor => new Doctor
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Rank = doctor?.Rank,
                Status = doctor.Status,
                ShortDescription = doctor?.ShortDescription,
                YearsOfExperience = doctor?.YearsOfExperience,
                Photo = doctor?.Photo,
                LongDescription = doctor?.LongDescription,
                EmergencyContact = doctor?.EmergencyContact,
                PhoneNumber = doctor?.PhoneNumber,
                WorkingHours = doctor?.WorkingHours,
                DaysAvailable = doctor?.DaysAvailable,
                WorkingDays = doctor?.WorkingDays,
                Country = doctor?.Country,
                UserId = doctor?.UserId
            }).ToList();
        }
        else
        {
            doctors = _context.Doctors.FromSqlInterpolated($"exec dbo.spDoctor_GetAllDoctorsOnExperience")
            .AsEnumerable().Select(doctor => new Doctor
            {
                DoctorId = doctor.DoctorId,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                Rank = doctor?.Rank,
                Status = doctor.Status,
                ShortDescription = doctor?.ShortDescription,
                YearsOfExperience = doctor?.YearsOfExperience,
                Photo = doctor?.Photo,
                LongDescription = doctor?.LongDescription,
                EmergencyContact = doctor?.EmergencyContact,
                PhoneNumber = doctor?.PhoneNumber,
                WorkingHours = doctor?.WorkingHours,
                DaysAvailable = doctor?.DaysAvailable,
                WorkingDays = doctor?.WorkingDays,
                Country = doctor?.Country,
                UserId = doctor?.UserId
            }).ToList();
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
                PhotoUrl = doctor?.Photo,
                LongDescription = doctor?.LongDescription,
                EmergencyContact = doctor?.EmergencyContact,
                PhoneNumber = doctor?.PhoneNumber,
                WorkingHours = doctor?.WorkingHours,
                DaysAvailable = doctor?.DaysAvailable,
                WorkingDays = doctor?.WorkingDays,
                Country = doctor?.Country,
                UserId = doctor?.UserId
            });
        }
        var categories = _context.DoctorCategories.ToList();

        var categoryViewModelList = new List<DoctorCategoryViewModel>();
        foreach (var category in categories)
        {
            categoryViewModelList.Add(new DoctorCategoryViewModel
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
                isActive = category.CategoryId == id ? true : false
            });
        }
        var viewModel = new SelectDoctorViewModel
        {
            Doctors = doctorViewModelList,
            doctorCategories = categoryViewModelList
        };

        return viewModel;
    }
}
