

using System.Text.RegularExpressions;
using NellsPay.Send.Models.LoginModels;

namespace NellsPay.Send.Services
{
    public class SignUpValidationService : ISignUpValidationService
    {
         public bool Validate(SignUpModel model, IToastService toastService)
    {
        if (string.IsNullOrWhiteSpace(model.Email))
        {
            model.EmailStatus = 2;
            toastService.ShowToast("Email is required.");
            return false;
        }

        if (!Regex.IsMatch(model.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
        {
            model.EmailStatus = 2;
            toastService.ShowToast("Please enter a valid email address.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(model.Password))
        {
            model.PasswordStatus = 2;
            toastService.ShowToast("Password is required.");
            return false;
        }

        if (model.Password.Length < 6)
        {
            model.PasswordStatus = 2;
            toastService.ShowToast("Password must be at least 6 characters.");
            return false;
        }

        if (string.IsNullOrWhiteSpace(model.ReEnterPassword))
        {
            model.ReEnterPasswordStatus = 2;
            toastService.ShowToast("Please re-enter your password.");
            return false;
        }

        if (model.ReEnterPassword != model.Password)
        {
            model.ReEnterPasswordStatus = 2;
            toastService.ShowToast("Passwords do not match.");
            return false;
        }

        return true;
    }

    public void ClearStatuses(SignUpModel model)
    {
        model.EmailStatus = string.IsNullOrWhiteSpace(model.Email) ? 0 : 1;
        model.PasswordStatus = string.IsNullOrWhiteSpace(model.Password) ? 0 : 1;
        model.ReEnterPasswordStatus = string.IsNullOrWhiteSpace(model.ReEnterPassword) ? 0 : 1;
    }
    }
}
