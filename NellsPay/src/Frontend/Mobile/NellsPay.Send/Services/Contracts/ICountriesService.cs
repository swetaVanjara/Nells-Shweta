using System;
using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;

namespace NellsPay.Send.Services.Contracts
{
    public interface ICountriesService
    {
        Task<CountriesResponse?> GetCountries(int PageIndex, int PageSize);
        Task<Data?> ToggleFavCurrency(Data country);
        Task<Data?> ToggleFavCountry(Data country);
        Task<MobileMoneyResponse?> GetMobileWalletProviders(string countryId);
        Task<BankTransferResponse?> GetFinancialInstitutions(string countryId);
    }
}