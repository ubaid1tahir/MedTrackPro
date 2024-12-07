using DataLibrary.Models.Doctor;
using DataLibrary.ViewModels.Doctor;
using MedTrackPro.Data;
using MedTrackPro.UtilityMethods;
using MedTrackPro.UtilityMethods.Interfaces;
using MedTrackPro.UtilityMethods.UtilModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Web;

namespace MedTrackPro.Controllers;

public class HospitalUserController : Controller
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IEmailSender _emailSender;
    public HospitalUserController(ApplicationDbContext context, UserManager<IdentityUser> userManager, IEmailSender emailSender)
    {
        _context = context;
        _userManager = userManager;
        _emailSender = emailSender;
    }
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    
    public async Task<IActionResult> AddNewCategory([FromBody] CategoryRequest categoryRequest)
    {
        if (string.IsNullOrWhiteSpace(categoryRequest.CategoryName))
        {
            return BadRequest(new { message = "Category name cannot be empty." });
        }
        try
        {
            var category = _context.DoctorCategories.Where(c => c.Name == categoryRequest.CategoryName).FirstOrDefault();
            if(category != null)
            {
                return BadRequest(new { message = "Category name is already registered." });
            }
            _context.DoctorCategories.Add(new DoctorCategory
            {
                Name = categoryRequest.CategoryName,
                Description = categoryRequest.CategoryName
            });
            await _context.SaveChangesAsync();

            return Ok(new {message = "Category Added Successfully"});
        }catch(Exception ex)
        {
            return StatusCode(500, new { message = "An error occurred while adding the category. Please try again." });
        }
            
        
    }

    public IActionResult AddDoctor()
    {
        var viewModel = new DoctorViewModel
        {
            DoctorCategories = _context.DoctorCategories.ToList()
        };
        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddDoctor(DoctorViewModel model)
    {
        if (ModelState.IsValid)
        {
            var adminId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if(adminId == null)
            {
                return NotFound();
            }
            IdentityUser doctorUser = new IdentityUser
            {
                Email = model.EmailAddress,
                UserName = model.EmailAddress
            };
            
            var user = await _userManager.CreateAsync(doctorUser, model.Password);
            if (!user.Succeeded)
            {
                ModelState.AddModelError("Invalid Form Submission", "The email already exists");
                return View(model);
            }
            var doctor = new Doctor()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                EmailAddress = model.EmailAddress,
                LicenseNumber = model.LicenseNumber,
                Status = model.Status,
                Rank = model.Rank,
                AddedById = adminId
            };
            doctor.CategoryId = 
                _context.DoctorCategories.Where(c => c.Name == model.Category).FirstOrDefault().CategoryId;
            _context.Doctors.Add(doctor);
            await _context.SaveChangesAsync();

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(doctorUser);

            var encodedToken = HttpUtility.UrlEncode(token);

            var callbackUrl = Url.Action(
                nameof(ConfirmEmail),
                "HospitalUser",
                new { userId = doctorUser.Id, token = encodedToken },
                protocol: Request.Scheme);


            var emailHtmlContent = IdentityMethods.GenerateWelcomeEmailHtml(model.FirstName, callbackUrl);

            await _emailSender.SendEmailAsync(
                model.EmailAddress,
                "Verify Your Email to get started with MedTrackPro", emailHtmlContent);

            await _userManager.AddToRoleAsync(doctorUser, CommonMethods.DoctorRole);
            TempData["RegistrationMsg"] = "Registration Successful. Email has been sent to the doctor email.";
            return RedirectToAction("Index");
        }
        ModelState.AddModelError("Invalid Form Submission", "All fields are required");
        return View(model);
    }

    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        if(userId == null || token == null)
        {
            return RedirectToAction("Index", "Home");
        }

        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return NotFound($"Unable to find a user with the userId {userId}");
        }
        var decodedToken = HttpUtility.UrlDecode(token);

        var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

        if (result.Succeeded)
        {
            var emailHtmlContent = IdentityMethods.GenerateWelcomeConfirmedEmailHtml(user.UserName);

            await _emailSender.SendEmailAsync(user.Email, "Welcome to MedTrackPro – Let’s Revolutionize Healthcare Together!" , emailHtmlContent);

            TempData["Message"] = "Your email has been confirmed. You can now log in.";
            return RedirectToAction("Login", "Account");
        }
        TempData["Message"] = "Error confirming your email.";
        return RedirectToAction("Login", "Account");
    }
}
