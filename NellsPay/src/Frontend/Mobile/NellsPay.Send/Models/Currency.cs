using NellsPay.Send.ResponseModels;
using SQLite;

namespace NellsPay.Send.Models
{
    public partial class Currency : BaseViewModel
    {
        [PrimaryKey] public Guid Id { get; set; }
        public string Country { get; set; } = default!;
        public string Country2Code { get; set; } = default!;
        public string Country3Code { get; set; } = default!;
        public string CurrencyCode { get; set; } = default!;
        public string CurrencyName { get; set; } = default!;
        public string CurrencySymbol { get; set; } = default!;
        public string CurrencyFlag { get; set; } = default!;
        private string region { get; set; } = default!;
        public string phoneCode { get; set; }
        public string Region
        {
            get { return region; }
            set
            {
                if (region != value)
                {
                    region = value;
                    OnPropertyChanged();

                }
            }
        }
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

        // This will NOT be stored in SQLite
        [Ignore]
        public List<PaymentMethod>? paymentMethods { get; set; }

        public string CountryFlag { get; set; } = default!;

       [ObservableProperty]
        private bool isFavorite;

    }
    
}
