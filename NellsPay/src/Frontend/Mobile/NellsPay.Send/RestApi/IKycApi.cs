using System;
using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;
using Refit;

namespace NellsPay.Send.RestApi
{
    public interface IKycApi
    {
        [Get("/kyc/profiles/customer/{customerId}")]
        Task<ProfileByIdResponse> GetProfilebyId([Header("Authorization")] string authorization, string customerId);
        
        [Get("/kyc/profiles/{profileId}/veriff-session")]
        Task<SessionByProfileIdResponse> GetSessionByProfileId([Header("Authorization")] string authorization, string profileId);

        [Post("/kyc/profiles/sessions")]
        Task<SessionResponse> PostStartSession([Header("Authorization")] string authorization, [Body] SessionWrapper sesssion);

        [Post("/kyc/profiles/{profileId}/upload-image")]
        Task<DocUploadResponse> UploadImage([Header("Authorization")] string authorization, string profileId, [Body] DocUploadWrapper wrapper);

        [Patch("/kyc/profiles/{profileId}/session")]
        Task<StartSessionResponse> SubmitSession([Header("Authorization")] string authorization, string profileId, [Body] SubmitSessionWrapper wrapper);
        
    }
}