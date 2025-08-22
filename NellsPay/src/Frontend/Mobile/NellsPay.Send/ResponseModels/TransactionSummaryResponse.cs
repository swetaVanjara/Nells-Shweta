using System;
namespace NellsPay.Send.ResponseModels
{ 
    public partial class TransactionSummaryResponse
    {
        public TransactionSummary? TransactionSummary { get; set; }
    }

    public partial class CurrencySummary
    {
        public string? Currency { get; set; }
        public string? CurrencySymbol { get; set; }
        public long TotalAmount { get; set; }
        public long TransactionCount { get; set; }
    }

    public partial class TransactionSummary
    {
        public CurrencySummary[]? CurrencySummaries { get; set; }
        public long GrandTotalInUsd { get; set; }
        public long TotalTransactionCount { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
    }
}