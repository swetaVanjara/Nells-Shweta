using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Models
{
    public class TransactionDetailModel : BaseModel
    {
        public string SenderFlag { get; set; } = default!;
        public string ReciverFlag { get; set; } = default!;
        public string Title { get; set; } = default!;
        public string Image { get; set; } = default!;
        public string Initials { get; set; } = default!;
        public string Amount { get; set; } = default!;
        public string ISO { get; set; } = default!;
        public string Date { get; set; } = default!;
        public string Time { get; set; } = default!;
        public string Transferamount { get; set; } = default!;
        public string Totaltorecipient { get; set; } = default!;
        public string SenderCountry { get; set; } = default!;
        public string SenderCurrency { get; set; } = default!;
        public string ReciverCurrency { get; set; } = default!;
        public string TransactionID { get; set; } = default!;
        public string Status { get; set; } = default!;
        public string RecipientName { get; set; } = default!;
        public string Deliverymethod { get; set; } = default!;
        public string Accountnumber { get; set; } = default!;
        public string Transactionnumber { get; set; } = default!;
        public string Reasonoftransaction { get; set; } = default!;
        public string Additionalnote { get; set; } = default!;
        public DateTime TransactionDate { get; set; } = default!;
        public string TransferDate { get; set; } = default!;
        public string TransferTime { get; set; } = default!;
        public string PayOutAccount { get; set; } = default!;
    }
}
