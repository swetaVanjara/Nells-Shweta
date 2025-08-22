using System;
namespace NellsPay.Send.ResponseModels
{
    public partial class PaymentsProvidersResponse
    {
        public Providers Providers { get; set; }
    }

    public partial class Providers
    {
        public long PageIndex { get; set; }
        public long PageSize { get; set; }
        public long Count { get; set; }
        public Datas[] Data { get; set; }
    }

    public partial class Datas
    {
        public Guid Id { get; set; }
        public long ProviderId { get; set; }
        public string Code { get; set; }
        public string CountryCode { get; set; }
        public string DynamicRegistrationCode { get; set; }
        public string IdentificationCode { get; set; }
        public string Name { get; set; }
        public bool Regulated { get; set; }
        public bool PopularProvider { get; set; }
        public Uri LogoUrl { get; set; }
        public string PaymentTemplate { get; set; }
    }
}