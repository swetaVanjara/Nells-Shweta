

using System.Text.RegularExpressions;
using NellsPay.Send.Models.LoginModels;
using NellsPay.Send.Models.RecipientsModels;

namespace NellsPay.Send.Services
{
    public class RecipientValidationService : IRecipientValidationService
    {
        public bool Validate(AddEditRecipientModel model, IToastService toastService)
        {
            if (string.IsNullOrWhiteSpace(model.FirstName))
            {
                model.FirstNameStatus = 2;
                toastService.ShowToast("First Name is required.");
                return false;
            }
            if (!Regex.IsMatch(model.FirstName, @"^[a-zA-Z\s]+$"))
            {
                model.FirstNameStatus = 2;
                toastService.ShowToast("First Name must contain only Latters.");
                return false;                
            }
            if (string.IsNullOrWhiteSpace(model.LastName))
            {
                model.LastNameStatus = 2;
                toastService.ShowToast("Last Name is required.");
                return false;
            }
            if (!Regex.IsMatch(model.LastName, @"^[a-zA-Z\s]+$"))
            {
                model.LastNameStatus = 2;
                toastService.ShowToast("Last Name must contain only Latters.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(model.PhoneNumber))
            {
                model.PhoneNumberStatus = 2;
                toastService.ShowToast("Phone Number is required.");
                return false;
            }
            if (!Regex.IsMatch(model.PhoneNumber, @"^\d+$"))
            {
                model.PhoneNumberStatus = 2;
                toastService.ShowToast("Phone Number must contain only numbers.");
                return false;
            }
            if (string.IsNullOrWhiteSpace(model.DeliveryMethod))
            {
                model.DeliveryMethodStatus = 2;
                toastService.ShowToast("Payment Method is required.");
                return false;
            }
            // if (string.IsNullOrWhiteSpace(model.AccountNumber))
            // {
            //     model.AddressStatus = 2;
            //     toastService.ShowToast("Account Number is required.");
            //     return false;
            // }
            if (!string.IsNullOrWhiteSpace(model.PostalCode))
            {
                model.PostalCodeStatus = 2;
                toastService.ShowToast("Please Enter Postal Code.");
                return false;
            }
            if (!string.IsNullOrWhiteSpace(model.Email))
            { 
                if (!Regex.IsMatch(model.Email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                {
                    model.EmailStatus = 2;
                    toastService.ShowToast("Please enter a valid email address.");
                    return false;
                }
            }
            return true;
        }
        public void ClearStatuses(AddEditRecipientModel model)
        {
            model.EmailStatus = string.IsNullOrWhiteSpace(model.Email) ? 0 : 1;
        }
    }
}