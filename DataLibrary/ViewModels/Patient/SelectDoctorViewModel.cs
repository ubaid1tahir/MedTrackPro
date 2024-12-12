using DataLibrary.ViewModels.Doctor;

namespace DataLibrary.ViewModels.Patient;

public class SelectDoctorViewModel
{
    public List<DoctorViewModel> Doctors { get; set; } = new List<DoctorViewModel>();

    public List<DoctorCategoryViewModel> doctorCategories { get; set; } = new List<DoctorCategoryViewModel>();
}
