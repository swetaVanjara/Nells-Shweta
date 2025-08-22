using System;
using SQLite;
namespace NellsPay.Send.ResponseModels
{
    public class CountriesResponse
    {
        public Countries? countries { get; set; }
    }

    public class Countries
    {
        public int pageIndex { get; set; }
        public int pageSize { get; set; }
        public int count { get; set; }
        public List<Data>? data { get; set; }
    }

    public class Data
    {
        [PrimaryKey] public string? id { get; set; }
        public string? countryName { get; set; }
        public string? country2Code { get; set; }
        public string? country3Code { get; set; }
        public string? countryFlag { get; set; }
        public string? phoneCode { get; set; }
        public string? region { get; set; }
        public string? subRegion { get; set; }
        public string? currencyCode { get; set; }
        public string? currencyName { get; set; }
        public string? currencySymbol { get; set; }
        public string? currencyFlag { get; set; }
        public string? direction { get; set; }

        public bool isFavCountry { get; set; }
        public bool isFavCurrency { get; set; }

        // This will NOT be stored in SQLite
        [Ignore] public List<PaymentMethod>? paymentMethods { get; set; }
        [Ignore] public List<FinancialInstitution>? financialInstitutions { get; set; }
        [Ignore] public List<MobileWalletProvider>? mobileWalletProviders { get; set; }

        // This will be stored in SQLite as a string
        public string PaymentMethodsJson
        {
            get => paymentMethods == null
                ? string.Empty
                : JsonSerializer.Serialize(paymentMethods);
            set => paymentMethods = string.IsNullOrWhiteSpace(value)
                ? new List<PaymentMethod>()
                : JsonSerializer.Deserialize<List<PaymentMethod>>(value);
        }


        // This will be stored in SQLite as a string
        public string FinancialInstitutionJson
        {
            get => financialInstitutions == null
                ? string.Empty
                : JsonSerializer.Serialize(financialInstitutions);

            set => financialInstitutions = string.IsNullOrWhiteSpace(value)
                ? new List<FinancialInstitution>()
                : JsonSerializer.Deserialize<List<FinancialInstitution>>(value);
        }

        // This will be stored in SQLite as a string
        public string mobileWalletProvidersJson
        {
            get => mobileWalletProviders == null
                ? string.Empty
                : JsonSerializer.Serialize(mobileWalletProviders);

            set => mobileWalletProviders = string.IsNullOrWhiteSpace(value)
                ? new List<MobileWalletProvider>()
                : JsonSerializer.Deserialize<List<MobileWalletProvider>>(value);
        }
    }

    public class FinancialInstitution
    {
        public string? id { get; set; }
        public string? countryId { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? type { get; set; }
        public string? status { get; set; }
    }

    public class MobileWalletProvider
    {
        public string? id { get; set; }
        public string? countryId { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? status { get; set; }
    }

    public class PaymentMethod
    {
        public string? id { get; set; }
        public string? countryId { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? type { get; set; }
        public string? status { get; set; }
    }
    public class MobileMoneyResponse
    {
        public List<PaymentMethod>? mobileWalletProviders { get; set; }
    }
    public class BankTransferResponse
    {
        public List<PaymentMethod>? financialInstitutions { get; set; }
    }
}
