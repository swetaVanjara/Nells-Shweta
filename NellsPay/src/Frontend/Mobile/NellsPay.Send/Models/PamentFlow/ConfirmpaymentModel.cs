using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Models.PamentFlow
{
    public class ConfirmpaymentModel
    {
        public string? Amount { get; set; }
        public string? Currency { get; set; }
        public string? Payeename { get; set; }
        public string? PaymentMethod { get; set; }
        public string? Bankname { get; set; }
        public string? Accountnumber { get; set; }


    }
}
