using Microsoft.AspNetCore.Mvc;

namespace MedTrackPro.Controllers;

public class HospitalUserController : Controller
{
    public IActionResult Index()
    {
        return View();
    }


}
