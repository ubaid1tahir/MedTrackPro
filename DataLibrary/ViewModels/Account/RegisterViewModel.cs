﻿
namespace DataLibrary.ViewModels.Account;
public class RegisterViewModel
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string ConfirmPassword { get; set; }

    public bool AcceptTermsAndConditions { get; set; }
}
