using Microsoft.AspNetCore.Mvc;

namespace MedTrackPro.Controllers
{
    public class ErrorController : Controller
    {
        public IActionResult NotFound()
        {
            return View();
        }
    }
}
