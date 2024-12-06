using DataLibrary.Models.Account;
using DataLibrary.Models.Patient;
using DataLibrary.ViewModels.Account;
using DNTCaptcha.Core;
using MedTrackPro.Data;
using MedTrackPro.UtilityMethods;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;


namespace MedTrackPro.Controllers;

public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly ApplicationDbContext _context;
    private readonly DNTCaptchaOptions _dNTCaptchaOptions;
    private readonly IDNTCaptchaValidatorService _validatorService;

    public AccountController(UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager, IDNTCaptchaValidatorService validatorService,
        IOptions<DNTCaptchaOptions> options, ApplicationDbContext context)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _validatorService = validatorService;
        _context = context;
        _dNTCaptchaOptions = options == null ? throw new ArgumentNullException(nameof(options)) : options.Value;
    }


    public IActionResult Login()
    {
        LoginViewModel model = new();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ValidateDNTCaptcha(ErrorMessage = "Please Enter valid security number")]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null)
            {
                var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("Contacts", "Patient"); // Redirect to home or another page after login
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "User does not exist.");
            }
        }
        return View();
    }

    public IActionResult Register()
    {
        RegisterViewModel model = new();
        return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    [ValidateDNTCaptcha(ErrorMessage = "Please Enter valid security number")]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = new IdentityUser { UserName = model.Email, Email = model.Email };
            var result = await _userManager.CreateAsync(user, model.Password);
            var patient = new PatientModel
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Age = model.Age,
                GuardenerName = model.GuardenerName,
                Address = model.Address,
                AdditionalNote = model?.AdditionalNote,
                UserId = user.Id,
                isMarried = model.isMarried,
                PhoneNumber = model.PhoneNumber,
                Severity = model.Severity
            };
            await _userManager.AddToRoleAsync(user, CommonMethods.PatientRole);
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return RedirectToAction("Contacts", "Patient"); // Redirect to home or another page after registration
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
        return View(model);
    }

    [HttpPost]
    // Logout
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return RedirectToAction("Index", "Home"); // Redirect to home page after logout
    }

    public IActionResult TermsAndCondition()
    {
        return View();
    }

    public IActionResult AccessDenied()
    {
        return View();
    }
}
