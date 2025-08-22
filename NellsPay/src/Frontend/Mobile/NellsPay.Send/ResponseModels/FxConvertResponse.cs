namespace NellsPay.Send.ResponseModels
{
    public partial class FxConvertResponse
    {
        public FxConvert FxConvert { get; set; }
    }

    public partial class FxConvert
    {
        public string FromCurrency { get; set; }
        public string ToCurrency { get; set; }
        public string CurrencyPair { get; set; }
        public long Amount { get; set; }
        public double ConvertedAmount { get; set; }
        public double Rate { get; set; }
        public DateTimeOffset Date { get; set; }
        public long Timestamp { get; set; }
    }
}
