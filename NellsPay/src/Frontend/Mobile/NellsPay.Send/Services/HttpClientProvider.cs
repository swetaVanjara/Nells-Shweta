using System;
using NellsPay.Send.Constants;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Refit;

namespace NellsPay.Send.Services
{
    public class HttpClientProvider
    {
        private static readonly Lazy<HttpClientProvider> _instance = new(() => new HttpClientProvider());
        public static HttpClientProvider Instance => _instance.Value;

        private readonly HttpClient _httpClient;
        private readonly JsonSerializerSettings _jsonSerializerSettings;

        private HttpClientProvider()
        {
            _httpClient = new HttpClient(new HttpLoggingHandler())
            {
                BaseAddress = new Uri(Server.ApiUrl),
            };
            _jsonSerializerSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Unspecified,
            };
        }
        public T GetApi<T>()
        {
            return RestService.For<T>(_httpClient, new RefitSettings
            {
                ContentSerializer = new NewtonsoftJsonContentSerializer(_jsonSerializerSettings)
            });
        }
    }
}