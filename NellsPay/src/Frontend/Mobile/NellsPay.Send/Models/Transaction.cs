namespace NellsPay.Send.Models
{
   public class Transactions : BaseModel 
    {
        public string TransactionNumber { get; set; } = default!;

        public string Status { get; set; } = default!;

        public string StatusFlag { get; set; }  = default!;

        public Recipient Recipient { get; set; } = default!;    

        public double? SenderAmount { get; set; } 

        public string SenderCurrency { get; set; }   = default!;
        public string ReciverCurrency { get; set; }   = default!;

        public string SenderCountry { get; set; } = default!;

        public double? ReceiverAmount { get; set; }  

        public double? TransactionFee { get; set; }

        public double? ExchangeRate { get; set; }
        public string Date { get; set; } = default!;
        public string Time { get; set; } = default!;
    }
}
