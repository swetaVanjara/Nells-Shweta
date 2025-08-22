using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Models.MoneyTransferFlowModels
{
    public class ReviewTransictionModel : BaseModel
    {
        public string SendISOCode { get; set; } = default!;
        public string ReciveISOCode { get; set; } = default!;
        public List<SendingReasonModel> SendingReasons { get; set; } = new()
        {
            new SendingReasonModel { ReasonName = "Loan payment" },
            new SendingReasonModel { ReasonName = "Travel payment" },
            new SendingReasonModel { ReasonName = "School fees" },
            new SendingReasonModel { ReasonName = "Tax payment" },
            new SendingReasonModel { ReasonName = "Family Support" },
            new SendingReasonModel { ReasonName = "Bride Price" },
            new SendingReasonModel { ReasonName = "Investment" },
            new SendingReasonModel { ReasonName = "Medical payment" },
            new SendingReasonModel { ReasonName = "Gift" },
            new SendingReasonModel { ReasonName = "Real estate purchase" },
            new SendingReasonModel { ReasonName = "Education" },
            new SendingReasonModel { ReasonName = "Insurance payment" },
            new SendingReasonModel { ReasonName = "Utility payment" }
        };
        public string Reason { get; set; } = default!;
        public double? Transferamount { get; set; } = default!;
        public double? Reciveamount { get; set; } = default!;
        public string Totaltorecipient { get; set; } = default!;
        public double? Exchangerate { get; set; } = default!;
        public double? Transferfee { get; set; } = default!;
        public string Transfertaxes { get; set; } = default!;
        public string Bankfee { get; set; } = default!;
        public string Voucher { get; set; } = default!;
        public string Total { get; set; } = default!;
        public Recipient? Recipient { get; set; } = default!;
        public string DeliveryMethod { get; set; } = default!;
        public string Transfertime { get; set; } = default!;
        public string TransactionNumber { get; set; } = default!;

    }
    public class SendingReasonModel
    {
        public string ReasonName { get; set; } = string.Empty;
    }
}
