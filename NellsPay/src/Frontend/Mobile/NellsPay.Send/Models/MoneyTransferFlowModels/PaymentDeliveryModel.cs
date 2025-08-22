using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Models.MoneyTransferFlowModels
{
    public class PaymentDeliveryModel : BaseViewModel
    {
        public string Title { get; set; } = default!;
        public string Icon { get; set; } = default!;
    }
}
