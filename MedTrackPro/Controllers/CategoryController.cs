using Microsoft.AspNetCore.Mvc;

namespace MedTrackPro.Controllers;

public class CategoryController : Controller
{
    public IActionResult FindDoctors(int id)
    {
        return View();
    }
}


