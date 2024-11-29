
using System.ComponentModel.DataAnnotations;

namespace DataLibrary.Models.Account;

public class LoginModel
{
    [Required]
    public string Email { get; set; }

    public string Password { get; set; }

    public bool RememberMe { get; set; } = false;

}
