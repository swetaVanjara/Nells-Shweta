using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Models.MoneyTransferFlowModels
{
    public class TransferPinModel : BaseModel
    {
        public string Receiver { get; set; } = default!;
        public string ISOCode { get; set; } = default!;
        public string Transferamount { get; set; } = default!;
        public string TransferID { get; set; } = default!;
        public string TransferDate { get; set; } = default!;
        public string Sender { get; set; } = default!;
        
        public string ExchangeRate { get; set; } = default!;
        public string PaymentMethod { get; set; } = default!;
        public string Tax { get; set; } = default!;
        public string Reason { get; set; } = default!;
    }
}
