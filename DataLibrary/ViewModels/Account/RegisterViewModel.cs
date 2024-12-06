
namespace DataLibrary.ViewModels.Account;
public class RegisterViewModel
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }
    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Address { get; set; }

    public int Age { get; set; }
    public string? GuardenerName { get; set; }

    public string PhoneNumber { get; set; }

    public bool isMarried { get; set; }

    public decimal Weight { get; set; }

    public decimal Height { get; set; }

    public string? AdditionalNote { get; set; }
    public enum Gender;

    public int Severity { get; set; } // 1-10

    public bool AcceptTermsAndConditions { get; set; }
}

