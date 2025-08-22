using System;
using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;
using Refit;

namespace NellsPay.Send.RestApi
{
    interface ICountriesAPI
    {
        [Get("/countries")]
        Task<CountriesResponse?> GetCountries([Header("Authorization")] string authorization, [Query] int PageIndex, [Query] int PageSize);

        [Get("/countries/{countryId}/mobile-wallet-providers")]
        Task<MobileMoneyResponse?> GetMobileWalletProviders([Header("Authorization")] string authorization, string countryId);
        
        [Get("/countries/{countryId}/financial-institutions")]
        Task<BankTransferResponse?> GetFinancialInstitutions([Header("Authorization")] string authorization,string countryId);
    }
}