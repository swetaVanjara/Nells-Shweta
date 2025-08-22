using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;
using NellsPay.Send.RestApi;

namespace NellsPay.Send.Services
{
    public class KycService(IUserService userService, IToastService toastService) : BaseService, IKycService
    {
        private readonly IKycApi _KycrApi = HttpClientProvider.Instance.GetApi<IKycApi>();
        private readonly IToastService _toastService = toastService;

        public async Task<ProfileByIdResponse> GetProfilebyId(string customerId)
        {
            try
            {
                await userService.RefreshToken();
                return await _KycrApi.GetProfilebyId(Auth, customerId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<SessionByProfileIdResponse> GetSessionByProfileId(string profileId)
        {
            try
            {
                await userService.RefreshToken();
                return await _KycrApi.GetSessionByProfileId(Auth, profileId);
            }
            catch (Refit.ApiException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                
                // Return a manually created 404-like object
                return new SessionByProfileIdResponse
                {
                    Title = ex.ReasonPhrase,
                    Status = 404,
                    Detail = ex.Message
                };
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<StartSessionResponse?> PatchSubmitSession(SubmitSessionWrapper request, string ProfileID)
        {
            try
            {
                await userService.RefreshToken();
                return await _KycrApi.SubmitSession(Auth, ProfileID, request);
            }
            catch (Exception e)
            {
                _toastService.ShowToast( "Error :" + e.Message.ToString());
                return null;
            }
        }

        public async Task<SessionResponse?> PostSessionStart(SessionWrapper request)
        {
            try
            {
                await userService.RefreshToken();
                return await _KycrApi.PostStartSession(Auth, request);
            }
            catch (Exception e)
            {
                _toastService.ShowToast( "Error :" + e.Message.ToString());
                return null;
            }
        }

        public async Task<DocUploadResponse?> PostUploadImage(DocUploadWrapper request, string ProfileID)
        {
            try
            {
                await userService.RefreshToken();
                return await _KycrApi.UploadImage(Auth, ProfileID, request);
            }
            catch (Exception e)
            {
                _toastService.ShowToast( "Error :" + e.Message.ToString());
                return null;
            }
        }
    }
}