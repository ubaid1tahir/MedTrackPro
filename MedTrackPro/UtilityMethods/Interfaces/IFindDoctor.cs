using DataLibrary.ViewModels.Patient;
using MedTrackPro.Data;

namespace MedTrackPro.UtilityMethods.Interfaces;

public interface IFindDoctor
{
    SelectDoctorViewModel FindDoctors(ApplicationDbContext _context, int? id = 0);
}
