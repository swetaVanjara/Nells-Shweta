using NellsPay.Send.ResponseModels;
using NellsPay.Send.RestApi;

namespace NellsPay.Send.Services
{
    public class CustomerService(IUserService userService, IToastService toastService) : BaseService, ICustomerService
    {
        private readonly ICustomerApi _customerApi = HttpClientProvider.Instance.GetApi<ICustomerApi>();
        private readonly IToastService _toastService = toastService;

        public async Task<CustomerResponse?> GetCustomerByEmail(string email)
        {
            try
            {
                await userService.RefreshToken();
                return await _customerApi.GetCustomerByEmail(Auth, email);
            }
            catch (Exception e)
            {
                _toastService.ShowToast( "Error :" + e.Message.ToString());
                return null;
            }
        }

        public async Task<CustomerResponse?> GetCustomerById(string id)
        {
            try
            {
                await userService.RefreshToken();
                return await _customerApi.GetCustomerById(Auth, id);
            }
            catch (Exception e)
            {
                _toastService.ShowToast( "Error :" + e.Message.ToString());
                return null;
            }
        }

        public async Task<SaltbyemailResponse> GetSaltbyemail(string email)
        {
            try
            {
                await userService.RefreshToken();
                return await _customerApi.GetSaltbyemail(Auth, email);
            }
            catch (Exception e)
            {
                _toastService.ShowToast( "Error :" + e.Message.ToString());
                return null;
            }
        }
        public async Task<CustomerIdResponse?> AddSECustomer(AddSeCustomerWrapper addSeCustomer, string customerId)
        {
            try
            {
                await userService.RefreshToken();
                var response = await _customerApi.AddSECustomer(Auth, addSeCustomer, customerId);
                return response;
            }
            catch (Exception e)
            {
                _toastService.ShowToast("Error :" + e.Message.ToString());
                return null;
            }
        }

        public async Task<CreateTransactionResponse> CreateTransaction(CreateTransactionWrapper createTransactionWrapper)
        {
            try
            {
                await userService.RefreshToken();
                Console.WriteLine(createTransactionWrapper.ToString());
                var response = await _customerApi.CreateTransaction(Auth, createTransactionWrapper);
                return response;
            }
            catch (Exception e)
            {
                _toastService.ShowToast("Error :" + e.Message.ToString());
                return null;
            }
        }

        public async Task<GetTransactionResponse> GetTransaction(int PageIndex, int PageSize)
        {
            try
            {
                await userService.RefreshToken();
                return await _customerApi.GetTransaction(Auth, PageIndex,PageSize);
            }
            catch (Exception e)
            {
                _toastService.ShowToast( "Error :" + e.Message.ToString());
                return null;
            }
        }

        public async Task<EditCustomerResponse> EditCustomer(EditCustomerWrapper editCustomerWrapper)
        {
            try
            {
                await userService.RefreshToken();
                return await _customerApi.EditCustomer(Auth, editCustomerWrapper);
            }
            catch (Exception e)
            {
                _toastService.ShowToast( "Error :" + e.Message.ToString());
                return null;
            }
        }
    }
}
