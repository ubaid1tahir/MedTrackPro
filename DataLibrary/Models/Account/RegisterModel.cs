

using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models.Account;

public class RegisterModel
{
    [Required]
    public string Email { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword {  get; set; }

    public bool AcceptTermsAndConditions {  get; set; }
}
