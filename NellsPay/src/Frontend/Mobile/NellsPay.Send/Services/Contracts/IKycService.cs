using System;
using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;
using NellsPay.Send.RestApi;

namespace NellsPay.Send.Services.Contracts
{
    public interface IKycService
    {
        Task<SessionByProfileIdResponse> GetSessionByProfileId( string profileId);
        Task<ProfileByIdResponse> GetProfilebyId(string customerId);
        Task<SessionResponse?> PostSessionStart(SessionWrapper request);
        Task<DocUploadResponse?> PostUploadImage(DocUploadWrapper request, string ProfileID);
        Task<StartSessionResponse?> PatchSubmitSession(SubmitSessionWrapper request, string ProfileID);
    }
}