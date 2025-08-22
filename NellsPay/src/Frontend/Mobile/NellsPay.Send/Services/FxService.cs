using NellsPay.Send.Repository;
using NellsPay.Send.ResponseModels;
using NellsPay.Send.RestApi;

namespace NellsPay.Send.Services
{
    public class FxService(IUserService userService, IFxRepository fxRepository) : BaseService, IFxService
    {

        private readonly IFxAPI _FxApi = HttpClientProvider.Instance.GetApi<IFxAPI>();
        public async Task<FxConvertResponse?> ConvertCurrency(string FromCurrency, string ToCurrency, double Amount, bool forceRefresh)
        {
            try
            {
                if (forceRefresh)
                {
                    await userService.RefreshToken();
                    return await _FxApi.ConvertCurrency(Auth, FromCurrency, ToCurrency, Amount);
                }
                else
                {
                    var fxConvert = await fxRepository.GetFxConvertsAsync();
                    if (fxConvert == null)
                    {
                        await userService.RefreshToken();
                        var data = await _FxApi.ConvertCurrency(Auth, FromCurrency, ToCurrency, Amount);

                        await fxRepository.InsertAll(new List<FxConvert> { data?.FxConvert ?? new FxConvert() });
                        return data;
                    }
                    else
                    {
                        return new FxConvertResponse
                        {
                            FxConvert = fxConvert,
                        };
                    }
                }

            }
            catch
            {
                return null;
            }
        }
    }
}