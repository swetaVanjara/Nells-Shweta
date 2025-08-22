using NellsPay.Send.Models.LoginModels;
using NellsPay.Send.Models.RecipientsModels;

namespace NellsPay.Send.Services.Contracts
{
    public interface IRecipientValidationService
    {
        bool Validate(AddEditRecipientModel model, IToastService toastService);
        void ClearStatuses(AddEditRecipientModel model);
    }
}
