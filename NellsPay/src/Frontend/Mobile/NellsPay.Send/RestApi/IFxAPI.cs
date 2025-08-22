using System;
using NellsPay.Send.Contracts;
using NellsPay.Send.ResponseModels;
using Refit;

namespace NellsPay.Send.RestApi
{
    public interface IFxAPI
    { 
        [Get("/fx/convert")]
        Task<FxConvertResponse?> ConvertCurrency([Header("Authorization")] string authorization,[Query] string FromCurrency,[Query] string ToCurrency,[Query] double Amount);
    }
}