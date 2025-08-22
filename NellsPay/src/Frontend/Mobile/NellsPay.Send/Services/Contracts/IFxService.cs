using System;
using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;

namespace NellsPay.Send.Services.Contracts
{
    public interface IFxService
    {
        Task<FxConvertResponse?> ConvertCurrency(string FromCurrency,string ToCurrency,double Amount , bool forceRefresh);
    }
}