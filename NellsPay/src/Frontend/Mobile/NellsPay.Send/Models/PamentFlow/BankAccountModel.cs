using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NellsPay.Send.Models.PamentFlow
{
    public class BankAccountModel
    {
        public BankModel Bank { get; set; } = new BankModel();
        public string AccountNumber { get; set; } = string.Empty;
        public string AccountHolderName { get; set; } = string.Empty;
        //public string BankCode { get; set; } = string.Empty;
        //public string BranchCode { get; set; } = string.Empty;
        //public bool IsDefault { get; set; } = false;
        //public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
