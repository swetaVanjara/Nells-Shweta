using NellsPay.Send.ResponseModels;

namespace NellsPay.Send.Services.Contracts
{
    public interface IRecipientService
    {
        Task<RecipientResponse> GetRecipientsAsync();
         Task<AddRecipientResponse?> PostRecipientsAsync(RecipientWrapper request);
        Task<EditRecipientResponse?> PutRecipientsAsync(RecipientWrapper request);
    }
}
