using NellsPay.Send.Repository;
using NellsPay.Send.ResponseModels;
using NellsPay.Send.RestApi;

namespace NellsPay.Send.Services
{
    public class CountriesService(IUserService userService, IToastService toastService, ICountryRepository countryRepository) : BaseService, ICountriesService
    {
        private readonly ICountriesAPI _CountryrApi = HttpClientProvider.Instance.GetApi<ICountriesAPI>();
        private readonly IToastService _toastService = toastService;

        public async Task<CountriesResponse?> GetCountries(int PageIndex, int PageSize)
        {
            try
            {
                var countries = await countryRepository.GetAllCountries();
                if (countries == null || !countries.Any())
                {
                    await userService.RefreshToken();
                    var countryData = await _CountryrApi.GetCountries(Auth, 0, PageSize);
                    await countryRepository.InsertAll(countryData?.countries?.data);
                    return countryData;
                }
                else
                {
                    return new CountriesResponse
                    {
                        countries = new Countries
                        {
                            pageIndex = PageIndex,
                            pageSize = PageSize,
                            count = countries.Count,
                            data = countries
                        },
                    };
                }

            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<MobileMoneyResponse?> GetMobileWalletProviders(string countryId)
        {
            try
            {
                await userService.RefreshToken();
                return await _CountryrApi.GetMobileWalletProviders(Auth, countryId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<BankTransferResponse?> GetFinancialInstitutions(string countryId)
        {
            try
            {
                await userService.RefreshToken();
                return await _CountryrApi.GetFinancialInstitutions(Auth, countryId);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Data?> ToggleFavCurrency(Data country)
        {
            try
            {
                return await countryRepository.ToggleFavCurrency(country);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<Data?> ToggleFavCountry(Data country)
        {
            try
            {
                return await countryRepository.ToggleFavCountry(country);
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}