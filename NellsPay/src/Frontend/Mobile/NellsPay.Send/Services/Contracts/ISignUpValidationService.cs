using NellsPay.Send.Models.LoginModels;

namespace NellsPay.Send.Services.Contracts
{
    public interface ISignUpValidationService
    {
        bool Validate(SignUpModel model, IToastService toastService);
        void ClearStatuses(SignUpModel model);
    }
}
