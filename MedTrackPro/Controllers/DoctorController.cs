using Microsoft.AspNetCore.Mvc;

namespace MedTrackPro.Controllers;

public class DoctorController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
