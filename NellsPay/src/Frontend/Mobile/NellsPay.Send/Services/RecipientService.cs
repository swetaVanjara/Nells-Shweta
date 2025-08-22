

using NellsPay.Send.ResponseModels;
using NellsPay.Send.RestApi;

namespace NellsPay.Send.Services
{

    public class RecipientService(ISettingsProvider settingsProvider, IUserService userService) : BaseService, IRecipientService
    {

        private readonly ICustomerApi _customerApi = HttpClientProvider.Instance.GetApi<ICustomerApi>();

        public async Task<RecipientResponse> GetRecipientsAsync()
        {
            try
            {
                await userService.RefreshToken();
                return await _customerApi.GetRecipientsByCustomerId(Auth, customerId: settingsProvider.CustomerId);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

         public async Task<AddRecipientResponse?> PostRecipientsAsync(RecipientWrapper request)
        {
            try
            {
                await userService.RefreshToken();
                Debug.Write(request.ToString());
                var response = await _customerApi.AddRecipientsByCustomerId(Auth, customerId: settingsProvider.CustomerId ?? "", request);
                return response;       
            }
            catch (Exception ex)
            {
                return null;
            }
         
        }

        public async Task<EditRecipientResponse?> PutRecipientsAsync(RecipientWrapper request)
        {
            try
            {
                await userService.RefreshToken();
                Console.Write(_customerApi.ToString());
                Console.Write(request.ToString());
                return await _customerApi.EditRecipientsByCustomerId(Auth, customerId: settingsProvider.CustomerId ?? "", request);
            }
            catch (Exception ex)
            {
                return null;
            }
          
        }
       
    }
}
