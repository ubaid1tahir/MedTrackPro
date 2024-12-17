using DataLibrary.ViewModels.Patient;
using MedTrackPro.Data;

namespace MedTrackPro.UtilityMethods.Interfaces;

public interface IDoctor
{
    SelectDoctorViewModel FindDoctors(ApplicationDbContext _context, int? id = 0);
}
